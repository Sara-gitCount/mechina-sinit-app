import { Component, inject } from '@angular/core';
import { LotteryService } from '../../../Services/lotteryService/lottery.service';
import { DtoLottery } from '../../../models/LotteryModel';
import { CommonModule } from '@angular/common';
import { Observable,firstValueFrom  } from 'rxjs';
import { Useres } from '../../../models/UsersModel';

@Component({
  selector: 'app-lottery',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './lottery.component.html',
  styleUrl: './lottery.component.scss'
})
export class LotteryComponent {
  private lotteryService=inject(LotteryService);
  result='';
  lottery:DtoLottery[] = [];
  revenue=-1;
  doLottery$!: Observable<boolean>;

  ngOnInit(){   
    this.doLottery$ = this.lotteryService.LotteryDone();
    this.doLottery$.subscribe(res => {
      if (res) {
        this.GetAllWinnersAsync();
     }
  });
  }

  // LotteryAsync(){
  //   this.lotteryService.LotteryAsync().subscribe((l)=>{
  //      if(l){
  //       this.result="הגרלה בוצעה בהצלחה"
  //       this.doLottery$ = this.lotteryService.LotteryDone();
  //       this.GetAllWinnersAsync();
  //       this.GetAllRevenueAsync();
  //       this.SendingEmailAsync();
  //     }
  //     else{
  //       this.result="הגרלה נכשלה"
  //     }
  //     this.result=l;
  //   })
  // }

  LotteryAsync() {
  this.lotteryService.LotteryAsync().subscribe((l) => {
    if (l) {
      this.result = "הגרלה בוצעה בהצלחה";

      this.doLottery$ = this.lotteryService.LotteryDone();
      this.GetAllRevenueAsync();

      // עכשיו מביאים זוכים
      this.GetAllWinnersAsync().subscribe((winners) => {
        this.lottery = winners;

        // ורק עכשיו שולחים מיילים
        this.SendingEmailAsync();
      });

    } else {
      this.result = "הגרלה נכשלה";
    }
  });
}


  onlyForManager(){
    const manager = JSON.parse(localStorage.getItem('user') || '{}');
    return manager.roles === 'manager';
  }
 

  // GetAllWinnersAsync(){
    
  //   this.lotteryService.GetAllWinnersAsync().subscribe((l)=>{
  //     this.lottery=l;
  //         console.log(!!this.doLottery$,"after get");

  //   })
  // }

  GetAllWinnersAsync() {
  return this.lotteryService.GetAllWinnersAsync();
}


  GetAllRevenueAsync(){
    this.lotteryService.GetAllRevenueAsync().subscribe((l)=>{
      this.revenue=l;
    })
  }

async SendingEmailAsync() {
  for (let i = 0; i < this.lottery.length; i++) {
    try {
      console.log(this.lottery[i].user.email,"email");

      const response = await firstValueFrom(
        this.lotteryService.SendingEmailAsync(
          this.lottery[i].user,
          this.lottery[i].giftName
        )
      );

      console.log(response);

      await new Promise(resolve => setTimeout(resolve, 4000));
    } catch (error) {
      console.error("Error sending email to", this.lottery[i].user.email, error);
      // ממשיכים לשאר המשתמשים גם אם אחד נכשל
    }
  }
}


}

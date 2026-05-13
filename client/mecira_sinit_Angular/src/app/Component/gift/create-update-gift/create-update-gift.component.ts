import { Component, inject } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { GiftServiceService } from '../../../Services/giftService/gift-service.service';
import { DtoGiftsUpdate } from '../../../models/GiftModel';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-create-update-gift',
  standalone: true,
  imports: [FormsModule,CommonModule],
  templateUrl: './create-update-gift.component.html',
  styleUrl: './create-update-gift.component.scss'
})
export class CreateUpdateGiftComponent {
constructor(private route:ActivatedRoute, private routGift:Router){}
  private giftServise=inject(GiftServiceService)
  result="";
  gift:DtoGiftsUpdate={name:"",description:"",image:"",price:0,donorId:0,categoryId:0}
  id=1

  ngOnInit(){
    const idGift=Number(this.route.snapshot.paramMap.get('id'));
    this.id=idGift;
    if(idGift>0)  
      this.getById(idGift);
}

    getById(id:number){
        this.giftServise.GetByIdAsync(id).subscribe((gift)=>{
        this.gift=gift 
      console.log("01");
         }) 
    }
   UpdateGiftAsync(){ //לא לתת לו לעדכן את ה ID
    this.giftServise.UpdateGiftAsync(this.id,this.gift).subscribe((result)=>{
      this.result=result
      this.goToGifts()
    })
  }

  
CreateGiftAsync(){
  this.giftServise.CreateGiftAsync(this.gift).subscribe((result)=>{
    this.result=result
    this.goToGifts()
  })
}

goToGifts(){
  this.routGift.navigate(['/gifts'])
}

  getImagePath(): string {
      return this.gift.image
    ? `assets/gift-images/${this.gift.image}.jpg`
    : 'assets/gift-images/placeholder.jpg'; // אופציונלי
  }
}

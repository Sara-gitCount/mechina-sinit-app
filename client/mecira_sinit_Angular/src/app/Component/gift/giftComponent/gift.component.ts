import { Component, inject } from '@angular/core';
import { DtoGifts, DtoGifts_D, DtoGiftsUpdate } from '../../../models/GiftModel';
import { GiftServiceService } from '../../../Services/giftService/gift-service.service';
import { DtoDonors } from '../../../models/DonorModel';
import { ActivatedRoute, RouterModule } from '@angular/router';
import { OrderService } from '../../../Services/orderService/order.service';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-gift',
  standalone: true,
  imports: [RouterModule,CommonModule],
  templateUrl: './gift.component.html',
  styleUrl: './gift.component.scss'
})
export class GiftComponent {
private giftService=inject(GiftServiceService)
private orderService=inject(OrderService);
listGift:DtoGifts[]=[];
gift:DtoGifts={id:0,name:"", description:"", image:"", price:0,donorId:0, categoryName:""}
result=""
a=1;
all=false;
loggedIn=false


ngOnInit(){
  this.getAllGifts();
  if(localStorage.getItem('user'))
    this.loggedIn=true;
}

getAllGifts(){
  this.giftService.GetAllGiftsAsync().subscribe((gifts)=>{
  this.listGift=gifts
  this.a=1
  this.all=true;
})}

GetGiftsByNameAsync(name:string){
      this.giftService.GetGiftsByNameAsync(name).subscribe((gift)=>{
        this.gift=gift
        this.a=4
        this.all=false;
})}

GetGiftsByDonorNameAsync(donorName:string){
  this.giftService.GetGiftsByDonorNameAsync(donorName).subscribe((gifts)=>{
    this.listGift=gifts
     this.a=1
     this.all=false;
  })
}

  GetOrderByPrice_CategoryAsync(){
    this.giftService.GetOrderByPrice_CategoryAsync().subscribe((gifts)=>{
      this.listGift=gifts
       this.a=1
      this.all=false;
    })
  }

  //הוספת מתנה לסל
    CreateOrderAsync(giftId:number){ 
    const user=JSON.parse(localStorage.getItem("user") || '{}');
    const idUser=user.id
    this.orderService.CreateOrderAsync(giftId,idUser).subscribe((result)=>{
      this.result=result
    })
  }

  onImageError(event: Event) {  //להוסיף ב HTML השאלה היא אם זה פה
  const img = event.target as HTMLImageElement;
  img.src = 'assets/gift-images/placeholder.png';
  }
}

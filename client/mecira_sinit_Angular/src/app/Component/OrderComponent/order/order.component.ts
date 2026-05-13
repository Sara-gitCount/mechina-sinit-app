import { Component, inject } from '@angular/core';
import { OrderService } from '../../../Services/orderService/order.service';
import { DtoUsers } from '../../../models/UsersModel';
import { DtoGifts, DtoGifts_D, DtoGiftsUpdate } from '../../../models/GiftModel';
import { GiftOrdersDto } from '../../../models/OrdersModel';
import { GiftServiceService } from '../../../Services/giftService/gift-service.service';
import { DtoDonors } from '../../../models/DonorModel';
import { RouterModule } from '@angular/router';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-order',
  standalone: true,
  imports: [RouterModule,CommonModule],
  templateUrl: './order.component.html',
  styleUrl: './order.component.scss'
})
export class OrderComponent {
private orderService=inject(OrderService);
private giftService=inject(GiftServiceService)
listOrderUsers:DtoUsers[]=[];
listGiftsUpdate:DtoGiftsUpdate[]=[];
listGiftOrders:GiftOrdersDto[]=[];
listGift:DtoGifts[]=[];
listGift_D:DtoGifts_D[]=[];
donor:DtoDonors={firstName:"",lastName:"",phone:"",email:"",donations:[]}
gift:DtoGifts={id:0,name:"", description:"", image:"", price:0,donorId:0, categoryName:""}
result=""
a=1;

ngOnInit(){
  this.getAllGifts();
}

getAllGifts(){
  this.giftService.GetAllGiftsAsync().subscribe((gifts)=>{
  this.listGift=gifts
  this.a=1
})}

GetDonorsAsync(name:string){
    this.giftService.GetGiftsByNameAsync(name).subscribe((gift)=>{
    const id=gift.donorId
    console.log(gift,"id");
    
    this.giftService.GetDonorsAsync(id).subscribe((donor)=>{
    this.donor=donor
     this.a=3
    })
})}

GetGiftsByNameAsync(name:string){
      this.giftService.GetGiftsByNameAsync(name).subscribe((gift)=>{
        this.gift=gift
         this.a=4
})}
    
GetGiftsByNumOfUsersAsync(numOfUsers:number){
  this.giftService.GetGiftsByNumOfUsersAsync(numOfUsers).subscribe((gifts)=>{
    this.listGift_D=gifts
     this.a=2
  })
}

GetGiftsByDonorNameAsync(donorName:string){
  this.giftService.GetGiftsByDonorNameAsync(donorName).subscribe((gifts)=>{
    this.listGift=gifts
     this.a=1
  })
}

  DeleteGiftAsync(id:number){
    this.giftService.DeleteGiftAsync(id).subscribe((result)=>{
        this.result=result
  this.giftService.GetAllGiftsAsync().subscribe((gifts)=>{
  this.listGift=gifts
  })
    })
  }

  GetOrderByPrice_CategoryAsync(){
    this.giftService.GetOrderByPrice_CategoryAsync().subscribe((gifts)=>{
      this.listGift=gifts
       this.a=1
    })
  }

GetAllUsersAsync(){
  this.orderService.GetAllUsersAsync().subscribe((orders)=>{
    this.listOrderUsers=orders
    this.a=5
  })
}

  GetGiftsOrderByOrdersAsync(){
    this.orderService.GetGiftsOrderByOrdersAsync().subscribe((gifts)=>{
      this.listGift_D=gifts;
      this.a=2;
    })
  }

  GetGiftsOrderByPriceAsync(){ 
    this.orderService.GetGiftsOrderByPriceAsync().subscribe((gifts)=>{
      this.listGiftsUpdate=gifts;
      this.a=6
    })
  }

  GetOrdersByGiftAsync(){
    this.orderService.GetOrdersByGiftAsync().subscribe((go)=>{
      this.listGiftOrders=go;
      this.a=7
    })
  }

    onImageError(event: Event) {  //להוסיף ב HTML השאלה היא אם זה פה
  const img = event.target as HTMLImageElement;
  img.src = 'assets/gift-images/placeholder.png';
  }
}

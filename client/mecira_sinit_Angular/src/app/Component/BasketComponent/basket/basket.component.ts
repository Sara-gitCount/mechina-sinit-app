import { Component, inject } from '@angular/core';
import { UserService } from '../../../Services/userService/user.service';
import { DtoBasket } from '../../../models/GiftModel';
import { OrderService } from '../../../Services/orderService/order.service';
import { Useres, UserResponseDto } from '../../../models/UsersModel';
import { trigger, transition, style, animate } from '@angular/animations';
import { CommonModule } from '@angular/common';
import { LotteryService } from '../../../Services/lotteryService/lottery.service';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-basket',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './basket.component.html',
  styleUrl: './basket.component.scss',
  animations: [
    trigger('fadeInOut', [
      transition(':enter', [
        style({ opacity: 0 }),
        animate('300ms', style({ opacity: 1 }))
      ]),
      transition(':leave', [
        animate('600ms', style({ opacity: 0 }))
      ])
    ])
  ]
})
export class BasketComponent {
private userService=inject(UserService)
basket:DtoBasket[]=[]
private orderService=inject(OrderService);
result=""
userId=0
private lotteryService=inject(LotteryService);
 doLottery$!: Observable<boolean>;
selectedItems: Set<number> = new Set();
showModal = false;
isOrderComplete = false;

ngOnInit(){
  this.doLottery$ = this.lotteryService.LotteryDone();
  const user = localStorage.getItem('user') 
  if(user!=null)
  this.userId=JSON.parse(user).id
  this.Basket()
}

Basket(){
  this.userService.Basket(this.userId).subscribe((basket)=>{
    this.basket=basket
  })
}

toggleSelection(orderId: number) {
  if(this.selectedItems.has(orderId)) {
    this.selectedItems.delete(orderId);
  } else {
    this.selectedItems.add(orderId);
  }
}

isSelected(orderId: number): boolean {
  return this.selectedItems.has(orderId);
}

// ← הוספה: פתיחת המודל
openCheckoutModal() {
  if(this.selectedItems.size === 0) {
    this.result = "יש לבחור לפחות פריט אחד";
    setTimeout(() => this.result = "", 3000);
    return;
  }
  this.showModal = true;
  this.isOrderComplete = false;
}

// ← הוספה: סגירת המודל
closeModal() {
  this.showModal = false;
  this.isOrderComplete = false;
}

// ← הוספה: קבלת הפריטים הנבחרים
getSelectedItems(): DtoBasket[] {
  return this.basket.filter(item => this.selectedItems.has(item.orderId));
}

// ← הוספה: חישוב סכום כולל
// ← תיקון: חישוב סכום כולל - תוודאי שהפונקציה הזו נכונה
getTotalPrice(): number {
  const total = this.getSelectedItems().reduce((sum, item) => {
    const itemTotal = item.price * item.amount;
    console.log(`Item: ${item.name}, Price: ${item.price}, Amount: ${item.amount}, Total: ${itemTotal}`);
    return sum + itemTotal;
  }, 0);
  
  console.log('Total Price:', total);
  return total;
}

// ← הוספה: ביצוע התשלום
completePurchase() {
  let completed = 0;
  const total = this.selectedItems.size;

  this.selectedItems.forEach(orderId => {
    this.orderService.UpdateOrderAsync(orderId).subscribe({
      next: (result) => {
        completed++;
        if(completed === total) {
          this.isOrderComplete = true;
          this.selectedItems.clear();
          setTimeout(() => {
            this.closeModal();
            this.Basket();
          }, 3000);
        }
      },
      error: (error) => {
        this.result = "שגיאה בהזמנת הפריטים";
        setTimeout(() => this.result = "", 3000);
        this.closeModal();
      }
    });
  });
}

  DeleteOrderAsync(orderId:number){
    this.orderService.DeleteOrderAsync(orderId).subscribe((result)=>{
    this.result="המוצר הוסר מהסל בהצלחה!"
    this.userService.Basket(this.userId).subscribe((basket)=>{
    this.basket=basket
    })
  })}
  
  UpdateOrderAsync(idOrder:number){
    this.orderService.UpdateOrderAsync(idOrder).subscribe((result)=>{
      this.result=result
        this.userService.Basket(this.userId).subscribe((basket)=>{
    this.basket=basket
  })
    })
  }

    CreateOrderAsync(giftId:number){ 
    const user=JSON.parse(localStorage.getItem("user") || '{}');
    const idUser=user.id
    this.orderService.CreateOrderAsync(giftId,idUser).subscribe((result)=>{
    this.result="המוצר נוסף לסל בהצלחה!";
    setTimeout(() => this.result = "", 3000);
    this.userService.Basket(this.userId).subscribe((basket)=>{
      this.basket=basket
    })
  })
  }
  
  onImageError(event: Event) {  //להוסיף ב HTML השאלה היא אם זה פה
  const img = event.target as HTMLImageElement;
  img.src = 'assets/gift-images/placeholder.png';
  }
}


import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { DtoUsers } from '../../models/UsersModel';
import { DtoGifts_D, DtoGiftsUpdate } from '../../models/GiftModel';
import { GiftOrdersDto } from '../../models/OrdersModel';

@Injectable({
  providedIn: 'root'
})
export class OrderService {
  private apiUrl="https://localhost:7261/Orders/ Orders"
  constructor(private http:HttpClient) { }

  GetAllUsersAsync():Observable<DtoUsers[]>{
    return this.http.get<DtoUsers[]>(`${this.apiUrl}/GetAllUsers`)
  }

  GetGiftsOrderByOrdersAsync():Observable<DtoGifts_D[]>{
    return this.http.get<DtoGifts_D[]>(`${this.apiUrl}/GetGiftsOrderByOrders`)
  }

  GetGiftsOrderByPriceAsync():Observable<DtoGiftsUpdate[]>{
    return this.http.get<DtoGiftsUpdate[]>(`${this.apiUrl}/GetGiftsOrderByPrice`)
  }

  DeleteOrderAsync(idOrder:number):Observable<string>{
    return this.http.delete(`${this.apiUrl}/${idOrder}`, {responseType:'text'})
  }

CreateOrderAsync(giftId:number,userId:number):Observable<string>{
  return this.http.post(`${this.apiUrl}/${giftId}/${userId}`,{}, {responseType:'text'})
}

UpdateOrderAsync(idOrder:number):Observable<string>{
  return this.http.put(`${this.apiUrl}/${idOrder}`,{} ,{responseType:'text'})
}

GetOrdersByGiftAsync():Observable<GiftOrdersDto[]>{
  return this.http.get<GiftOrdersDto[]>(`${this.apiUrl}/GetOrdersByGift`)
}
}

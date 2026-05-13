import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { DtoBasket } from '../../models/GiftModel';

@Injectable({
  providedIn: 'root'
})
export class UserService {
  private apiUrl="https://localhost:7261/users/Users"
  constructor(private http:HttpClient) { }

  Basket(idUser:number):Observable<DtoBasket[]>{
  return this.http.get<DtoBasket[]>(`${this.apiUrl}/Basket/${idUser}`)
}

}

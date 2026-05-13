import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, retry } from 'rxjs';
import { DtoGifts, DtoGifts_D, DtoGiftsUpdate } from '../../models/GiftModel';
import { DtoDonors } from '../../models/DonorModel';

@Injectable({
  providedIn: 'root'
})
export class GiftServiceService {

  private apiUrl='https://localhost:7261/gifts/Gifts';
  constructor(private http:HttpClient) { }

  GetAllGiftsAsync():Observable<DtoGifts[]>{
    return this.http.get<DtoGifts[]>(this.apiUrl);
  }

  GetDonorsAsync(id:number):Observable<DtoDonors>{  
    return this.http.get<DtoDonors>(`${this.apiUrl}/GetDonors/${id}`);
  }

  GetGiftsByNameAsync(name:string):Observable<DtoGifts>{   
    return this.http.get<DtoGifts>(`${this.apiUrl}/GetGiftsByName/${name}`)
  }

  GetGiftsByNumOfUsersAsync(numOfUsers:number):Observable<DtoGifts_D[]>{
    return this.http.get<DtoGifts_D[]>(`${this.apiUrl}/GetGiftsByNumOfUsersAsync?numOfUsers=${numOfUsers}`)
  }

  GetGiftsByDonorNameAsync(donorName:string):Observable<DtoGifts[]>{
    return this.http.get<DtoGifts[]>(`${this.apiUrl}/GetGiftsByDonorName?donorName=${donorName}`)
  }

  CreateGiftAsync(gift:DtoGiftsUpdate):Observable<string>{
    return this.http.post(this.apiUrl,gift, {responseType:'text'})
  }

  DeleteGiftAsync(id:number):Observable<string>{
    return this.http.delete(`${this.apiUrl}/${id}`, {responseType:'text'})
  }

  UpdateGiftAsync(id:number,gift:DtoGiftsUpdate):Observable<string>{
    return this.http.put(`${this.apiUrl}/${id}`,gift,{responseType:'text'})
  }

  GetOrderByPrice_CategoryAsync():Observable<DtoGifts[]>{
    return this.http.get<DtoGifts[]>(`${this.apiUrl}/GetOrderByPrice_CategoryAsync`)
  }

  GetByIdAsync(id:number):Observable<DtoGiftsUpdate>{
    return this.http.get<DtoGiftsUpdate>(`${this.apiUrl}/GetById/${id}`)
  }
}

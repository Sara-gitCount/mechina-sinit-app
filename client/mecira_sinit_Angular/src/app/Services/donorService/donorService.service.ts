import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { DtoDonors, DtoDonorsCreate, DtoDonorsUpdate } from '../../models/DonorModel';
import { Task } from 'zone.js/lib/zone-impl';


@Injectable({
  providedIn: 'root'
})
export class ServiceDonorService {

  private apiUrl = 'https://localhost:7261/donors/Donors';
  constructor(private http: HttpClient) {}
  GetAllDonors():Observable<DtoDonors[]>{
    return this.http.get<DtoDonors[]>(this.apiUrl);
  }

  GetByNameAsync(name:string):Observable<DtoDonorsCreate>{
    return this.http.get<DtoDonorsCreate>(`${this.apiUrl}/GetByName/${name}`);
  }

  GetByEmailAsync(email:string):Observable<DtoDonors>{
    return this.http.get<DtoDonors>(`${this.apiUrl}/GetByEmail/${email}`)
  }

  GetByGiftAsync(nameGift:string):Observable<DtoDonors>{
        console.log(nameGift);
    return this.http.get<DtoDonors>(`${this.apiUrl}/GetByGift/${encodeURIComponent(nameGift)}`);
  }

  CreateDonorAsync(donor:DtoDonorsCreate):Observable<string>{
    console.log("addDonor service",donor,donor.id)
    return this.http.post(this.apiUrl,donor,{ responseType: 'text' })
  }
//    return this.http.post<string>(this.apiUrl,donor,{ responseType: 'text' })

  DeleteDonorAsync(id:number):Observable<string>{
    return this.http.delete(`${this.apiUrl}/${id}`, {responseType:'text'})
  }

  UpdateDonorAsync(donor:DtoDonorsUpdate, id:number):Observable<string>{
    console.log(donor,id);
    return this.http.put(`${this.apiUrl}/${id}`,donor ,{responseType: 'text'})
  }

  AddDonotationAsync(donorId:number,giftId:number){
      const url = `${this.apiUrl}/AddDonotation/${donorId}/${giftId}`;
    return this.http.put<string>(url,null)
  }
}

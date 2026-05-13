import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { DtoLottery } from '../../models/LotteryModel';
import { Useres } from '../../models/UsersModel';

@Injectable({
  providedIn: 'root'
})
export class LotteryService {

  private apiUrl = 'https://localhost:7261/lottery/Lottery';
  //  private lotteryl = false;
  constructor(private http: HttpClient) { }

  LotteryAsync():Observable<string>{
     return this.http.post(this.apiUrl,{},{ responseType: 'text' as const }
  );
 }

  GetAllWinnersAsync():Observable<DtoLottery[]>{
    return this.http.get<DtoLottery[]>(`${this.apiUrl}/GetAllWinners`);
  }

  GetAllRevenueAsync():Observable<number>{
    return this.http.get<number>(`${this.apiUrl}/GetAllRevenue`);
  }

  LotteryDone():Observable<boolean>{
      console.log("HTTP CALLED");
    var a=this.http.get<boolean>(`${this.apiUrl}/LotteryDone`);
    return  a
  }

  SendingEmailAsync(user:Useres,nameGift:string):Observable<string>{
    return this.http.post( `${this.apiUrl}/sendingEmail/${nameGift}`,user,{ responseType: 'text' as const })
  }
}

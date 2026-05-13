import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Useres, UserResponseDto } from '../../models/UsersModel';
import { Observable } from 'rxjs';
import { LoginRequestDto, LoginResponseDto } from '../../models/AuthModel';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private apiUrl = "https://localhost:7261";
  constructor(private http: HttpClient) { }

  Login(loginDto:LoginRequestDto): Observable<LoginResponseDto>{
    return this.http.post<LoginResponseDto>(`${this.apiUrl}/login`,loginDto)
  }

  Register(user:Useres) :Observable<UserResponseDto>{
    console.log(user);
    return this.http.post<UserResponseDto>(`${this.apiUrl}/register`,user)
  }
}

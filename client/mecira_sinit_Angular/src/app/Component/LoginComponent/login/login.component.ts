import { Component, inject } from '@angular/core';
import { FormBuilder, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { LoginRequestDto } from '../../../models/AuthModel';
import { AuthService } from '../../../Services/authService/auth.service';
import { Router } from '@angular/router';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [FormsModule,ReactiveFormsModule,CommonModule],
  templateUrl: './login.component.html',
  styleUrl: './login.component.scss'
})
export class LoginComponent {

private loginService=inject(AuthService)
constructor(private router:Router, private fb:FormBuilder){}

loginForm=this.fb.group({
  email:['',[Validators.required,Validators.email]],
  password:['',[Validators.required,Validators.minLength(6)]],
})

login(){
  if(this.loginForm.invalid) return;
  const loginUser:LoginRequestDto={
  email:this.loginForm.value.email!,
  password:this.loginForm.value.password!,
}
 this.loginService.Login(loginUser).subscribe((user)=>{
    localStorage.setItem('authToket',user.token)
    localStorage.setItem('tokenType',user.tokenType)
    localStorage.setItem('expiresIn',String(user.expiresIn))
    localStorage.setItem('user',JSON.stringify(user.user))
    console.log(user);
    this.router.navigate(['/home']).then(() => window.location.reload());
  })}

}

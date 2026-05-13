import { Component, inject } from '@angular/core';
import { FormBuilder, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { Useres } from '../../../models/UsersModel';
import { AuthService } from '../../../Services/authService/auth.service';
import { LoginRequestDto } from '../../../models/AuthModel';
import { Route, Router } from '@angular/router';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-auth',
  standalone: true,
  imports: [FormsModule,ReactiveFormsModule,CommonModule],
  templateUrl: './auth.component.html',
  styleUrl: './auth.component.scss'
})

export class AuthComponent {
  private authService=inject(AuthService)
  constructor(private router: Router,private fb:FormBuilder){}
loginUser:LoginRequestDto={
  email:'',
  password:'',
}

  registerForm = this.fb.group({
      firstName:['',[Validators.required]],
      lastName:['',[Validators.required]],
      email:['',[Validators.required,Validators.email]],
      phone: ['',[Validators.required, Validators.pattern(/^(05\d{8}|02\d{7})$/)]],
      address:['',[Validators.required]],
      password:['',[Validators.required,Validators.minLength(6)]]
      
    })

Register(){
   if(this.registerForm.invalid){
     this.registerForm.markAllAsTouched(); 
      console.log("f");
    return;
   }
   const user:Useres={
    id:0,
    firstName:this.registerForm.value.firstName!
    ,lastName:this.registerForm.value.lastName!
    ,email:this.registerForm.value.email!
    ,phone:this.registerForm.value.phone!
    ,address:this.registerForm.value.address!
    ,roles:'client'
    ,password:this.registerForm.value.password!}
  this.loginUser.email=user.email
  this.loginUser.password=user.password
this.authService.Register(user).subscribe((user)=>
  this.authService.Login(this.loginUser).subscribe((user)=>{
    localStorage.setItem('authToket',user.token)
    localStorage.setItem('tokenType',user.tokenType)
    localStorage.setItem('expiresIn',String(user.expiresIn))
    this.router.navigate(['/home']).then(() => window.location.reload());
  })
)}

}


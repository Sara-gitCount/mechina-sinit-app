import { Component } from '@angular/core';
import { Routes } from '@angular/router';
import { DonorComponentComponent } from './Component/DonorComponent/donor-component/donor-component.component';
import { CreateUpdateComponent } from './Component/DonorComponent/donor-component/create-update/create-update.component';
import { GiftComponent } from './Component/gift/giftComponent/gift.component';
import { CreateUpdateGiftComponent } from './Component/gift/create-update-gift/create-update-gift.component';
import { LoginComponent } from './Component/LoginComponent/login/login.component';
import { AuthComponent } from './Component/AuthComponent/auth/auth.component';
import { BasketComponent } from './Component/BasketComponent/basket/basket.component';
import { OrderComponent } from './Component/OrderComponent/order/order.component';
import { LotteryComponent } from './Component/lotteryComponent/lottery/lottery.component';
import { LogoutComponent } from './Component/logout/logout.component';
import { HomeComponent } from './Component/home/home.component';


export const routes: Routes = [
    { path: '', redirectTo: '/home', pathMatch: 'full' },
    {path: 'home',component:HomeComponent},
    {path:'donors', component:DonorComponentComponent},
    {path: 'donorUpdate',component:CreateUpdateComponent},
    {path: 'donorAdd',component:CreateUpdateComponent},
    {path: 'gifts', component:GiftComponent},
    {path: 'addGift', component:CreateUpdateGiftComponent},
    {path: 'updateGift/:id', component:CreateUpdateGiftComponent},
    {path: 'login', component:LoginComponent},
    {path: 'register', component:AuthComponent},
    {path: 'basket', component:BasketComponent},
    {path: 'orders', component:OrderComponent},
    {path: 'lottery', component:LotteryComponent},
    {path: 'logout',component:LogoutComponent}
];

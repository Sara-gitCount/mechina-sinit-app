export interface DtoGifts_D {
    name:string;
    description:string;
    image:string;
    categoryId:number;
}

export interface DtoGifts {
    id:number
    name:string;
    description:string;
    image:string;
    price:number;
    donorId:number;
    categoryName:string;
}

export interface DtoGiftsUpdate{
    name:string;
    description:string;
    image:string;
    price:number;
    donorId:number;
    categoryId:number;
}

export interface DtoBasket{
     id:number;
     name:string;
     description:string;
     image:string;
     price:number;
     amount:number;
     orderId:number;
     giftId:number;
}
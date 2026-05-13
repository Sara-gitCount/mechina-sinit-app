import { DtoGifts_D } from "./GiftModel.js";

export interface DtoDonors {
firstName:string;
lastName:string;
phone:string;
email:string;
donations:DtoGifts_D[];
}

export interface DtoDonorsCreate {
    id:number;
    firstName:string;
    lastName:string;
    phone:string;
    email:string;
}

export interface DtoDonorsUpdate {
    firstName:string;
    lastName:string;
    phone:string;
    email:string;
}
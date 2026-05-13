import { UserResponseDto } from "./UsersModel";

export interface LoginRequestDto {
    email:string;
    password:string;
}

export interface LoginResponseDto{
    token:string
    tokenType:string
    expiresIn:number
    user:UserResponseDto
}

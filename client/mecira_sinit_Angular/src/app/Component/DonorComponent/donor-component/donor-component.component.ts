import { Component, inject } from '@angular/core';
import { ServiceDonorService } from '../../../Services/donorService/donorService.service';
import { DtoDonors, DtoDonorsCreate } from '../../../models/DonorModel';
import { RouterModule } from '@angular/router';

@Component({
  selector: 'app-donor-component',
  standalone: true,
  imports: [RouterModule],
  templateUrl: './donor-component.component.html',
  styleUrl: './donor-component.component.scss'
})
export class DonorComponentComponent {
ifTrue=0;
private donorService=inject(ServiceDonorService)
DonorList:DtoDonors[]=[];
donor:DtoDonors={firstName:"",lastName:"",phone:"",email:"",donations:[]};
donorCreate:DtoDonorsCreate={id:0,firstName:"",lastName:"",phone:"",email:""};
errorMessage = "";
notFound = false;

ngOnInit(){
  this.GetAllDonors();
}

GetAllDonors(){
  this.ifTrue=0
  this.donorService.GetAllDonors().subscribe((donors)=>{
    this.DonorList=donors;
    console.log("hgfcd");
    
  })
}

getByByName(value:string){
    if(!value || value=="")
      alert("יש להזין שם תקין");
      this.ifTrue=2
this.donorService.GetByNameAsync(value).subscribe((donor)=>{
  this.donorCreate=donor;
})
}
getByByEmail(value:string){
  if(!value || value=="")
    alert("יש להזין מייל תקין")
  console.log(value);
    this.ifTrue=1
this.donorService.GetByEmailAsync(value).subscribe((donor)=>
this.donor=donor)
}
getByByGift(value:string){ //זה מטומטם לפי מספר ולא לפי שם
  if(!value || value=="")
    alert("יש להזין מספר מתנה תקין")
    this.ifTrue=1
this.donorService.GetByGiftAsync(value).subscribe((donor)=>
this.donor=donor)
}

onDelete(firstName:string, lastName:string){
this.donorService.GetByNameAsync(firstName+" "+lastName).subscribe((donor)=>{
  this.donorService.DeleteDonorAsync(donor.id).subscribe(()=>{
    this.GetAllDonors();
})
})}
}

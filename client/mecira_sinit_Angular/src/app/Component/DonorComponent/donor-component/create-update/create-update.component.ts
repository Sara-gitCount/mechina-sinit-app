import { Component, inject } from '@angular/core';
import { DtoDonors, DtoDonorsCreate, DtoDonorsUpdate } from '../../../../models/DonorModel';
import { ServiceDonorService } from '../../../../Services/donorService/donorService.service';
import { ActivatedRoute, Router } from '@angular/router';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-create-update',
  standalone: true,
  imports: [FormsModule],
  templateUrl: './create-update.component.html',
  styleUrl: './create-update.component.scss'
})
export class CreateUpdateComponent {
constructor(private route: ActivatedRoute,private routeDonor:Router){}
donor:DtoDonorsCreate={id:0,firstName:"",lastName:"",phone:"",email:""};
private donorService=inject(ServiceDonorService)
donorUpdate:DtoDonorsUpdate={firstName:"", lastName:"",phone:"", email:""};
b=false;
result="";
name:string | null=null;

ngOnInit(){
  const donorName=this.route.snapshot.queryParamMap.get('name');
  this.name=donorName;
  if(donorName){
    this.donorService.GetByNameAsync(donorName).subscribe((donor)=>{
      this.donor=donor;
  })
}
}

onSubmit(id:number, firstName:string, lastName:string, phone:string, email:string){
  if(this.name==null){
    this.b=true;
        this.addDonor(id,firstName,lastName,phone,email)
  }
  else{
      this.b=false;
    this.updateDonor(id,firstName,lastName,phone,email)
  }
}

addDonor(id:number, firstName:string, lastName:string, phone:string, email:string){
this.donor.id=id;
this.donor.firstName=firstName;
this.donor.lastName=lastName;
this.donor.phone=phone;
this.donor.email=email;
this.donorService.CreateDonorAsync(this.donor).subscribe((donor)=>{
this.result=donor;
this.goToDonors()
})

}

updateDonor(id:number,firstName:string, lastName:string, phone:string, email:string){
this.donorUpdate.firstName=firstName;
this.donorUpdate.lastName=lastName;
this.donorUpdate.phone=phone;
this.donorUpdate.email=email;
//this.donorUpdate.donations=this.donorUpdate.donations;
console.log(this.donorUpdate,this.donor.id);
  this.donorService.UpdateDonorAsync(this.donorUpdate,this.donor.id).subscribe((donor)=>{
  this.result=donor
  this.goToDonors()
})
}

goToDonors(){
  this.routeDonor.navigate(['/donors'])
}
}

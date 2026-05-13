import { Component } from '@angular/core';
import { Router, RouterLink } from '@angular/router';

@Component({
  selector: 'app-logout',
  standalone: true,
  imports: [],
  templateUrl: './logout.component.html',
  styleUrl: './logout.component.scss'
})
export class LogoutComponent {
  constructor(private router:Router){}
  ngOnInit(){
    localStorage.clear();
    this.router.navigate(['/login']).then(() => window.location.reload());
  }

}

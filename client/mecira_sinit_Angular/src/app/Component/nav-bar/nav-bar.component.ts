import { CommonModule } from '@angular/common';
import { AfterViewInit, Component, ElementRef, HostListener, inject, QueryList, ViewChildren, ViewEncapsulation } from '@angular/core';
import { NavigationEnd, Router, RouterLink, RouterModule } from '@angular/router';
import { filter, Observable } from 'rxjs';
import { LotteryService } from '../../Services/lotteryService/lottery.service';

@Component({
  selector: 'app-nav-bar',
  standalone: true,
  imports: [RouterModule,CommonModule],
  templateUrl: './nav-bar.component.html',
  styleUrl: './nav-bar.component.scss',
  encapsulation: ViewEncapsulation.None
})
export class NavBarComponent implements AfterViewInit{

  @ViewChildren('navLink') navLinkElements!: QueryList<ElementRef>;
  
  activeIndex = 7;
  lightPosition = 1.5;
  role=''
  logedIn=false
  navLinks: any[] = [];
  // doLottery$!: Observable<boolean>;
  // private lotteryService=inject(LotteryService);
  
  ngOnInit(){
  // this.doLottery$ = this.lotteryService.LotteryDone();
   const user = JSON.parse(localStorage.getItem('user') || '{}');
   if (user && Object.keys(user).length > 0) {
      this.logedIn = true;
      this.role = user?.roles || '';
   }
    this.filterNavLinks();
      this.checkUserStatus();

  }

  filterNavLinks(){
  
    this.navLinks = this.allNavLinks.filter(link => {
      switch (link.showWhen) {
        case 'always':
          return true;
        case 'notLogged':
          return !this.logedIn;
        case 'logged':
          return this.logedIn;
        case 'manager':
          return this.logedIn && this.role === 'manager';
        case 'client':
          return this.logedIn && this.role === 'client';
        default:
          return false;
      }
    });
  }

    allNavLinks  = [
    { icon: 'bx-log-in', route: '/login', showWhen: 'notLogged'},
    { icon: 'bx-user-x', route: '/logout', showWhen: 'logged' },
    { icon: 'bx-user-plus', route: '/register', showWhen: 'notLogged'},
    { icon: 'bx-gift', route: '/gifts', showWhen: 'always' },
    { icon: 'bx-party', route: '/lottery', showWhen: 'always' },
    { icon: 'bx-list-ol', route: '/orders', showWhen: 'manager'},
    { icon: 'bx-cart', route: '/basket', showWhen: 'logged' },
    { icon: 'bx-home-heart', route: '/home', showWhen: 'always' },
    { icon: 'bx-user-pin', route: '/donors', showWhen: 'manager'}
  ];

  constructor(private router: Router) {
    // מעקב אחרי שינויי נתיב כדי לעדכן את ה-active
    this.router.events.pipe(
      filter(event => event instanceof NavigationEnd)
    ).subscribe(() => {
      this.updateActiveFromRoute();
    });
  }

  // פונקציה פשוטה לרענון
checkUserStatus() {
  const user = JSON.parse(localStorage.getItem('user') || '{}');
  if (user && Object.keys(user).length > 0) {
    this.logedIn = true;
    this.role = user?.roles || '';
  } else {
    this.logedIn = false;
    this.role = '';
  }
  this.filterNavLinks();
}

ngAfterViewInit() {
  setTimeout(() => {
    this.updateActiveFromRoute();
    
    // כוח את העדכון
    const homeIndex = this.navLinks.findIndex(link => link.route === '/home');
    if (homeIndex !== -1) {
      setTimeout(() => {
        this.updateLightPosition(homeIndex);
      }, 100);
    }
  }, 200);
}

updateActiveFromRoute() {
  const currentRoute = this.router.url;
  const index = this.navLinks.findIndex(link => link.route === currentRoute);
  
  if (index !== -1) {
    this.activeIndex = index;
    this.updateLightPosition(index);
  } else if (currentRoute === '/' || currentRoute === '/home') {
    const homeIndex = this.navLinks.findIndex(link => link.route === '/home');
    if (homeIndex !== -1) {
      this.activeIndex = homeIndex;
      this.updateLightPosition(homeIndex);
    }
  }
  
  // כוח עדכון של ה-DOM
  setTimeout(() => {
    this.updateLightPosition(this.activeIndex);
  }, 150); // ← הוסיפי את זה
}

  onLinkClick(index: number, route: string): void {
    this.activeIndex = index;
    this.updateLightPosition(index);
    this.router.navigate([route]);
  }

 
  private updateLightPosition(index: number): void {
  setTimeout(() => {
    const linkArray = this.navLinkElements.toArray();
    if (linkArray[index]) {
      const element = linkArray[index].nativeElement;
      const parent = element.parentElement; // ה-ul
      
      // חישוב מדויק יותר
      const parentLeft = parent?.offsetLeft || 0;
      const elementLeft = element.offsetLeft;
      const elementWidth = element.offsetWidth;
      
      // מיקום מדויק - ממורכז מעל האייקון
      const centerPosition = elementLeft + (elementWidth / 2) - 2; // 2em = חצי רוחב הפס
      
      this.lightPosition = centerPosition / 16; // המרה ל-em
    }
  }, 100); // ← הגדלתי ל-100ms
}
}

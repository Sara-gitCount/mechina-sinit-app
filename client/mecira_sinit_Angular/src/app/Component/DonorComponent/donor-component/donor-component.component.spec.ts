import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DonorComponentComponent } from './donor-component.component';

describe('DonorComponentComponent', () => {
  let component: DonorComponentComponent;
  let fixture: ComponentFixture<DonorComponentComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [DonorComponentComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(DonorComponentComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

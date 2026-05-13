import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CreateUpdateGiftComponent } from './create-update-gift.component';

describe('CreateUpdateGiftComponent', () => {
  let component: CreateUpdateGiftComponent;
  let fixture: ComponentFixture<CreateUpdateGiftComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [CreateUpdateGiftComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(CreateUpdateGiftComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

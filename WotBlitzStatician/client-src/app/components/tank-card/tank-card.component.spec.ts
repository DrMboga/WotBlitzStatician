import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { TankCardComponent } from './tank-card.component';

describe('TankCardComponent', () => {
  let component: TankCardComponent;
  let fixture: ComponentFixture<TankCardComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ TankCardComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(TankCardComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

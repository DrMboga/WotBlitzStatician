import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AccountTanksComponent } from './account-tanks.component';

describe('AccountTanksComponent', () => {
  let component: AccountTanksComponent;
  let fixture: ComponentFixture<AccountTanksComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AccountTanksComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AccountTanksComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

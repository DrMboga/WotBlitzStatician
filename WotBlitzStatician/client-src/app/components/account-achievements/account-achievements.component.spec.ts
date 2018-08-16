import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AccountAchievementsComponent } from './account-achievements.component';

describe('AccountAchievementsComponent', () => {
  let component: AccountAchievementsComponent;
  let fixture: ComponentFixture<AccountAchievementsComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AccountAchievementsComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AccountAchievementsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

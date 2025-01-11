import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AutomenuComponent } from './automenu.component';

describe('AutomenuComponent', () => {
  let component: AutomenuComponent;
  let fixture: ComponentFixture<AutomenuComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [AutomenuComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(AutomenuComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

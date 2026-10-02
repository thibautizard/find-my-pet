import { ComponentFixture, TestBed } from "@angular/core/testing";
import { BreederBadge } from "./breeder-badge";

describe("BreederBadge", () => {
  let component: BreederBadge;
  let fixture: ComponentFixture<BreederBadge>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [BreederBadge],
    }).compileComponents();

    fixture = TestBed.createComponent(BreederBadge);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it("should create", () => {
    expect(component).toBeTruthy();
  });
});

import { Directive } from '@angular/core';
import { NG_VALIDATORS } from '@angular/forms';
import { CreditCardValidator } from 'ngx-credit-cards';

@Directive({
  selector: '[cardNumValid][ngModel]',
  providers: [
    {
      provide: NG_VALIDATORS,
      useValue: CreditCardValidator.validateCardNumber,
      multi: true
    }
  ]
})
export class CardNumValidDirective {

  constructor() { }

}

import { Directive } from '@angular/core';
import { NG_VALIDATORS } from '@angular/forms';
import { CreditCardValidator } from 'ngx-credit-cards';

@Directive({
  selector: '[cardExpDateValid][ngModel]',
  providers: [
    {
      provide: NG_VALIDATORS,
      useValue: CreditCardValidator.validateCardExpiry,
      multi: true
    }
  ]
})
export class CardExpDateValidDirective {

  constructor() { }

}

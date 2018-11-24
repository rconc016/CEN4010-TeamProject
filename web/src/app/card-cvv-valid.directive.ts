import { Directive } from '@angular/core';
import { NG_VALIDATORS } from '@angular/forms';
import { CreditCardValidator } from 'ngx-credit-cards';

@Directive({
  selector: '[cardCvcValid][ngModel]',
  providers: [
    {
      provide: NG_VALIDATORS,
      useValue: CreditCardValidator.validateCardCvc,
      multi: true
    }
  ]
})
export class CardCvcValidDirective {

  constructor() { }

}

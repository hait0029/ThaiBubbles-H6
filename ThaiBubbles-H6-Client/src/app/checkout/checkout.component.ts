import { Component, OnInit } from '@angular/core';
import { NgIf } from '@angular/common';
import {
  FormBuilder,
  FormGroup,
  Validators,
  ReactiveFormsModule,
} from '@angular/forms';

@Component({
  selector: 'app-checkout',
  standalone: true,
  imports: [ReactiveFormsModule, NgIf],
  templateUrl: './checkout.component.html',
  styleUrl: './checkout.component.css',
})
export class CheckoutComponent implements OnInit {
  checkoutForm: FormGroup;
  paymentForm: FormGroup;
  isPaymentFormVisible = false; // Styrer visning af betalingsformularen
  isLoggedIn = true; // Simulerer loginstatus (skift til din logik)

  // Dummy-brugerdata til eksempler
  userData = {
    name: 'John Doe',
    address: 'Eksempelgade 123',
    zip: '1234',
    city: 'København',
    email: 'johndoe@example.com',
    phone: '12345678',
  };

  constructor(private fb: FormBuilder) {
    // Initialiserer formularerne her
    this.checkoutForm = this.fb.group({
      name: ['', Validators.required],
      address: ['', Validators.required],
      zip: ['', [Validators.required, Validators.pattern(/^\d{4}$/)]],
      city: ['', Validators.required],
      email: ['', [Validators.required, Validators.email]],
      phone: ['', [Validators.required, Validators.pattern(/^\d{8}$/)]],
    });

    this.paymentForm = this.fb.group({
      cardNumber: ['', [Validators.required, Validators.pattern(/^\d{16}$/)]],
      expiryDate: [
        '',
        [
          Validators.required,
          Validators.pattern(/^(0[1-9]|1[0-2])\/?([0-9]{2})$/),
        ],
      ],
      cvv: ['', [Validators.required, Validators.pattern(/^\d{3}$/)]],
      cardHolder: ['', Validators.required],
    });
  }

  ngOnInit() {
    // Hvis brugeren er logget ind, udfyld kundeoplysninger
    if (this.isLoggedIn) {
      this.checkoutForm.patchValue({
        name: this.userData.name,
        address: this.userData.address,
        zip: this.userData.zip,
        city: this.userData.city,
        email: this.userData.email,
        phone: this.userData.phone,
      });
    }
  }

  onSubmitCustomerInfo() {
    if (this.checkoutForm.valid) {
      // Skift til betalingsformularen
      this.isPaymentFormVisible = true;
    } else {
      alert('Udfyld venligst alle felter korrekt.');
    }
  }

  onSubmitPaymentInfo() {
    if (this.paymentForm.valid) {
      alert('Betaling er gennemført!');
    } else {
      alert('Udfyld venligst alle betalingsfelter korrekt.');
    }
  }
}

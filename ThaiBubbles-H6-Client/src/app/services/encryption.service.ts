import { Injectable } from '@angular/core';
import * as CryptoJS from 'crypto-js';

@Injectable({
  providedIn: 'root'
})
export class EncryptionService {
  // Key and IV must be exactly 32 bytes and 16 bytes, respectively
  private key = CryptoJS.enc.Utf8.parse('12345678901234567890123456789012'); // 32 bytes for AES-256
  private iv = CryptoJS.enc.Utf8.parse('1234567890123456'); // 16 bytes for AES-128

  // Encrypt a string
  encrypt(text: string): string {
    const encrypted = CryptoJS.AES.encrypt(text, this.key, {
      keySize: 256 / 8,
      iv: this.iv,
      mode: CryptoJS.mode.CBC,
      padding: CryptoJS.pad.Pkcs7,
    });

    return encrypted.toString();
  }

  // Decrypt a string
  decrypt(encryptedText: string): string {
    const decrypted = CryptoJS.AES.decrypt(encryptedText, this.key, {
      keySize: 256 / 8,
      iv: this.iv,
      mode: CryptoJS.mode.CBC,
      padding: CryptoJS.pad.Pkcs7,
    });

    return decrypted.toString(CryptoJS.enc.Utf8);
  }

  constructor() { }
}

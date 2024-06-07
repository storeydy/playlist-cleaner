import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'millisecond',
  standalone: true,
})
export class MillisecondPipe implements PipeTransform {
  transform(value: number): string {
    if (value == null) return '0:00';
    const minutes: number = Math.floor(value / 60000);
    const seconds: number = Math.floor((value % 60000) / 1000);
    return minutes + ":" + (seconds < 10 ? '0' : '') + seconds;
  }
}
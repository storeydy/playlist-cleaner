import { MillisecondPipe } from './millisecond.pipe';

describe('MillisecondPipe', () => {
  it('create an instance', () => {
    const pipe = new MillisecondPipe();
    expect(pipe).toBeTruthy();
  });
});

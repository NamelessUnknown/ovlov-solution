This solution consists of two added parts:
- api as an entry point to make call to calculation - nothing fancy, simple endpoint without any security, sanitizing, etc. I tried to fit in given timeframe.
- tests - were helpful when identifying errors, in fact milliseconds problem

At first I found that I can pass several dates, e.g. 2013-01-01, 2013-01-02, what was leading into trouble as calculator at first assumed single day data. I changed return type from single int to key value pair so calculation based on many dates could be made.

I'm aware of one more problem I introduced - first invervalDate occasionally is not handling dates properly. More touch is needed, but I ran out of time. Probably I would groupBy date at first and then iterate through new collection to get proper pairs of dates + taxes.

What is missing - bonus part - I ran out of time. How would I handle that:
Adding extra parameter to TraffiData which would be e.g some key value pair holding datetime and toll amount. This value would be passed down all the way to calculator which could use this data to evaluate toll for given city/area.

No error handling - obviously it would be nice to catch wrong date format, or try parse to format required by calculator. Same for sanitizing. Once again - I ran out of time to handle that. 

Thanks in advance, cool exercise.

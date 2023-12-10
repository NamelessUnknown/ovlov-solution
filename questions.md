This solution consists of two added parts:
- api as an entry point to make call to calculation - nothing fancy, simple endpoint without any security, sanitizing, etc. I tried to fit in given timeframe.
- tests - were helpful when identifying errors, in fact milliseconds problem

I did not touch target framework which is 3.1, it would be better to bump it, but I did not do that but wanted to let you know that I'm aware that it is outdated and it would be better to bump it.

Some naming may be incorrect - please keep in mind that, according to rules of play, I was limited by time.

At first I found that I can pass several dates, e.g. 2013-01-01, 2013-01-02, what was leading into trouble as calculator at first assumed single day data. I switched to key value pair so many dates could be inserted and calculated.

I'm aware of one more problem I introduced - first invervalDate occasionally is not handling dates properly. More touch is needed, but I ran out of time. Probably I would groupBy date at first and then iterate through new collection to get proper pairs of dates + taxes.

What is missing - bonus part - I ran out of time. How would I handle that:
Adding extra parameter to TraffiData which would be e.g some key value pair holding datetime and toll amount. This value would be passed down all the way to calculator which could use this data to evaluate toll for given city/area.

No error handling - obviously it would be nice to catch wrong date format, or try parse to format required by calculator. Same for sanitizing. Once again - I ran out of time to handle that. 

Thanks in advance, cool exercise.
# GetTimeDLL

# How to use?
This DLL gives you 5 options. You can:
1. Calculate exact time to next restart
 ```
    _restartHours = 12; // type in 1 hour that your server restarts on 
	_restartMinute = 00; // if you restart your server on different minute then typical 00 change it to your needs
	_restartEvery = 3; // type in how many hours there are between restarts on your server
	_getTime = call compile ("GetTime" callExtension format ["%1,%2,%3",_restartHours,_restartMinute,_restartEvery]);
	_hour = _getTime select 0;
	_mins = _getTime select 1;
	_secs = _getTime select 2;
	_timeToRestart = (_hour * 60 * 60) + (_mins * 60) + _secs;
```
2. Get current server time
```
_test4 = "GetTime" callExtension "time";
```
3. Get current hour
```
_test1 = "GetTime" callExtension "hour";
```
4. Get current minute
```
_test2 = "GetTime" callExtension "minute";
```
5. Get current second
```
_test3 = "GetTime" callExtension "second";
```

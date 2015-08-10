# Say
This outputs text at a specifed rate.  It is licensed under CC0 1.0 Universal.  If there is not a file named `License.txt` in this directory, please go to <https://creativecommons.org/publicdomain/zero/1.0/>.  
**Command Line:**  `Say interval text` or `Say /f [/s] file`.  Type `Say` for more information.  
**Return Values:**  0 = OK, -1 = Incorrect number of arguments, -2 = Invalid `interval` argument, -3 = No `file` argument, -4 = Could not find `file`, -5 = `file` has an invalid path, -6 = `file` has an invalid name, -7 = Unexpected I/O error reading from `file`.

# Version History:
## 1.x.x
#### Say 1.1.0
Added support for reading from files.  
Added capability to support other languages in the future.
#### Say 1.0.0
First version.

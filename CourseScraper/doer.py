## 30.01.2022
## Propio

import time
import pyautogui
import webbrowser
import random
import subprocess
pyautogui.FAILSAFE = False

#### URL TO SCRAP
url = input("ENTER URL:: ")

#### STARTING LIST INDEX
start = int(input("INDEX START:: "))


from urllib.request import Request, urlopen



def alarm():
    subprocess.Popen(["C:/Program Files (x86)/VideoLAN/VLC/vlc.exe","alarm.wav"])


userAgent = "Mozilla/5.0 (Linux; Android 7.0; SM-G930VC Build/NRD90M; wv) AppleWebKit/537.36 (KHTML, like Gecko) Version/4.0 Chrome/58.0.3029.83 Mobile Safari/537.36"

#### GET HTML FROM URL
req = Request(url, headers={'User-Agent':userAgent})
html = urlopen(req).read().decode("utf-8")

#### INITIALIZE VARIABLES
NumberOfElements = 0
linkList = []


#### USE REGULAR EXPRESSIONS TO FIND THE DOWNLOAD LINKS
import re
for element in re.findall( r'https://xxxxxxxxx.xx/[a-zA-Z0-9]*', html):
    if element != "https://xxxxxxxxx.xx/upgrade":
        #### APPEND DOWNLOAD LINK TO LIST IF IT IS NOT AN UPGRADE LINK
        print(element)
        NumberOfElements += 1
        linkList.append(element)


#### INFO
print("\n" + str(len(linkList)) + " LINKS FOUND \n")
        
#### FOR ALL LINKS FROM START 
for link in linkList[start: -1]:
    #### WAITS FOR AT LEAST 20 SECONDS AND GETS THE X,Y COORDINATES OF THE DOWNLOAD BUTTON, CLICKS, WAITS AT LEAST 35 SECONDS AND CLICKS
    webbrowser.open(link)
    time.sleep(20 + random.randint(0,5))
    x, y = pyautogui.locateCenterOnScreen('downbut.jpg', grayscale=True, confidence=.7)
    pyautogui.click(x, y)
    time.sleep(35 + random.randint(0,5))
    pyautogui.click(x, y)
    
    #### CHECK FOR CAPTCHA, SOUND ALARM IF FOUND
    time.sleep(5)
    if (pyautogui.locateCenterOnScreen('captcha.jpg', grayscale=True, confidence=.6) is not None):
        alarm()
        input("Captcha found...")
        
        
    #### WAIT FOR AT LEAST 20 SECONDS, LOCATE THE DOWNLOAD BUTTON AND CLICK
    time.sleep(20 + random.randint(0,5))
    x, y = pyautogui.locateCenterOnScreen('buttonnt.jpg', grayscale=True, confidence=.7)
    pyautogui.click(x, y)
    ## WAIT FOR AT LEAST 35 SECONDS AND CONTINUE NEXT LINK
    time.sleep(35 + random.randint(0,5)) 

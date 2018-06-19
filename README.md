# RTD266xFlash #

This is a combination of an Arduino project and a C# application to read and
write the firmware of the Realtek RTD266x flat panel display controller.

The Arduino code is based on ladyada's project:
https://github.com/adafruit/Adafruit_RTD266X_I2CFlasher

There is a special feature for a 3.5" HDMI display manufactured by
KeDei: you can replace the boot logo with a custom logo. The custom logo needs
to be 204x72 pixels and may only contain black and white pixels.

## Usage ##

### Arduino ###

Compile and download the Arduino sketch. You can close the Arduino IDE afterwards.

### Connecting ###

The communication is done via I²C which is accessible on the VGA and HDMI ports
of RTD266x. Connect SCL, SDA and GND with the corresponding Arduino pins.

For an Arduino Uno and an HDMI connector type A, this would be:

| Pin name | Arduino Uno | HDMI type A |
| --- | --- | --- |
| SCL | A5 | 15 |
| SDA | A4 | 16 |
| GND | GND | 17 |

There are no additional pull-ups required, they are already on the RTD266x
PCB.

### GUI tool ###

Select the COM port your Arduino is connected to and click **Connect**. You can
click **Read status** to check the connection. It should return values different
from 0x00 and 0xFF.

Enjoy!
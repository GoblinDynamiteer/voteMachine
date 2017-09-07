#include <U8glib.h>

/* Command buffer size / lenght */
#define SERIAL_BUFFER_SIZE 30

const char COMMAND_END = '\n';
U8GLIB_SSD1306_128X64 display(U8G_I2C_OPT_NONE);

void setup()
{
    Serial.begin(9600);
    Serial.setTimeout(1000);
}

void loop()
{
    /* Get command from bluetooth serial */
    char * data;
    data = readSerial();

    /* If command is not empty */
    if(data[0] != '\0')
    {
        setText(data);
    }

    delay(50);
}

/* Read serial command (from bluetooth) */
char * readSerial()
{
    char command[SERIAL_BUFFER_SIZE + 1];

    int size = Serial.readBytesUntil(
        COMMAND_END,
        command,
        SERIAL_BUFFER_SIZE
    );

    command[size] = 0;

    return command;
}

void setText(char * text)
{
    display.firstPage();

    do
    {
        display.setFont(u8g_font_helvB10);
        display.drawStr(0,15, text);
    }
    while(display.nextPage());

}

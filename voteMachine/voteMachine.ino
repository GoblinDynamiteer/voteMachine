#include <U8glib.h>

/* Command buffer size / length */
#define SERIAL_BUFFER_SIZE 30
#define LINE_HEIGHT 15

const char COMMAND_END = '\n';
U8GLIB_SSD1306_128X64 display(U8G_I2C_OPT_NONE);

char * line[4] = { "one", "two", "three", "four"};

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
    if(data[0])
    {
        setText();
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

    // Skriv ut size till display -- rad 3

    command[size] = 0;

    return command;
}

/* Write text to display */
void setText()
{
    display.firstPage();

    do
    {
        /* Använd 3 globala strängar för rader? */
        display.setFont(u8g_font_helvB10);

        display.drawStr(0, 1 * LINE_HEIGHT, line[0]);
        display.drawStr(0, 2 * LINE_HEIGHT, line[1]);
        display.drawStr(0, 3 * LINE_HEIGHT, line[2]);
        display.drawStr(0, 4 * LINE_HEIGHT, line[3]);
    }
    while(display.nextPage());

}

#g++ -I/usr/include/glib-2.0 -I/usr/lib64/glib-2.0/include -fpic -c g_object_link.cpp
#g++ -shared -o g_object_link.so g_object_link.o
CFLAGS = -I/usr/include/glib-2.0 -I/usr/lib64/glib-2.0/include -fpic

all: g_object_link.cpp
	g++ $(CFLAGS) -c g_object_link.cpp
	g++ -shared -o g_object_link.so g_object_link.o
	cp g_object_link.so ../bin/Debug/libg_object_link.so

clean:
	$(RM) g_object_link.so g_object_link.o

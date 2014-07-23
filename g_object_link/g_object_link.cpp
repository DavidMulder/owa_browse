#include <glib-object.h>

extern "C" void SetStringProperty(GObject*, char*, char*);
extern "C" void SetBoolProperty(GObject*, char*, bool);

void SetStringProperty(GObject *object, char* property_name, char* value)
{
    GValue gvalue = G_VALUE_INIT;
    g_value_init(&gvalue, G_TYPE_STRING);
    g_value_set_string(&gvalue, value);
    g_object_set_property(object, property_name, &gvalue);
}

void SetBoolProperty(GObject *object, char* property_name, bool value)
{
    GValue gvalue = G_VALUE_INIT;
    g_value_init(&gvalue, G_TYPE_BOOLEAN);
    g_value_set_boolean(&gvalue, value);
    g_object_set_property(object, property_name, &gvalue);
}


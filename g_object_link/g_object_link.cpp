#include <glib-object.h>
//#include <dlfcn.h>

extern "C" void SetStringProperty(GObject*, char*, char*);
extern "C" void SetBoolProperty(GObject*, char*, bool);
//extern "C" char* dom_element_get_attribute(GObject*, char*);

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

/*
char* dom_element_get_attribute(GObject *dom_element, char *name)
{
    void *handle;
    char* (*webkit_dom_element_get_attribute)(GObject*, gchar*);
    handle = dlopen("/usr/lib64/libwebkitgtk-3.0.so.0", RTLD_LAZY);
    if (!handle)
        return "";
    dlerror();
    webkit_dom_element_get_attribute = (char* (*)(GObject*, gchar*))dlsym(handle, "webkit_dom_element_get_attribute");
    if (dlerror() != NULL)
        return "";
    return (*webkit_dom_element_get_attribute)(dom_element, name);
} */

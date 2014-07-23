

# Warning: This is an automatically generated file, do not edit!

srcdir=.
top_srcdir=.

include $(top_srcdir)/config.make

ifeq ($(CONFIG),DEBUG_X86)
ASSEMBLY_COMPILER_COMMAND = dmcs
ASSEMBLY_COMPILER_FLAGS =  -noconfig -codepage:utf8 -warn:4 -optimize- -debug "-define:DEBUG;"
ASSEMBLY = bin/Debug/owa_browse3.exe
ASSEMBLY_MDB = $(ASSEMBLY).mdb
COMPILE_TARGET = winexe
PROJECT_REFERENCES = 
BUILD_DIR = bin/Debug

OWA_BROWSE3_EXE_MDB_SOURCE=bin/Debug/owa_browse3.exe.mdb
OWA_BROWSE3_EXE_MDB=$(BUILD_DIR)/owa_browse3.exe.mdb
ATK_SHARP_DLL_SOURCE=../../../../usr/lib/mono/gtk-sharp-3.0/atk-sharp.dll
GLIB_SHARP_DLL_SOURCE=../../../../usr/lib/mono/gtk-sharp-3.0/glib-sharp.dll
GDK_SHARP_DLL_SOURCE=../../../../usr/lib/mono/gtk-sharp-3.0/gdk-sharp.dll
GIO_SHARP_DLL_SOURCE=../../../../usr/lib/mono/gtk-sharp-3.0/gio-sharp.dll
CAIRO_SHARP_DLL_SOURCE=../../../../usr/lib/mono/gtk-sharp-3.0/cairo-sharp.dll
PANGO_SHARP_DLL_SOURCE=../../../../usr/lib/mono/gtk-sharp-3.0/pango-sharp.dll
GTK_SHARP_DLL_SOURCE=../../../../usr/lib/mono/gtk-sharp-3.0/gtk-sharp.dll

endif

ifeq ($(CONFIG),RELEASE_X86)
ASSEMBLY_COMPILER_COMMAND = dmcs
ASSEMBLY_COMPILER_FLAGS =  -noconfig -codepage:utf8 -warn:4 -optimize+
ASSEMBLY = bin/Release/owa_browse3.exe
ASSEMBLY_MDB = 
COMPILE_TARGET = winexe
PROJECT_REFERENCES = 
BUILD_DIR = bin/Release

OWA_BROWSE3_EXE_MDB=
ATK_SHARP_DLL_SOURCE=../../../../usr/lib/mono/gtk-sharp-3.0/atk-sharp.dll
GLIB_SHARP_DLL_SOURCE=../../../../usr/lib/mono/gtk-sharp-3.0/glib-sharp.dll
GDK_SHARP_DLL_SOURCE=../../../../usr/lib/mono/gtk-sharp-3.0/gdk-sharp.dll
GIO_SHARP_DLL_SOURCE=../../../../usr/lib/mono/gtk-sharp-3.0/gio-sharp.dll
CAIRO_SHARP_DLL_SOURCE=../../../../usr/lib/mono/gtk-sharp-3.0/cairo-sharp.dll
PANGO_SHARP_DLL_SOURCE=../../../../usr/lib/mono/gtk-sharp-3.0/pango-sharp.dll
GTK_SHARP_DLL_SOURCE=../../../../usr/lib/mono/gtk-sharp-3.0/gtk-sharp.dll

endif

AL=al
SATELLITE_ASSEMBLY_NAME=$(notdir $(basename $(ASSEMBLY))).resources.dll

PROGRAMFILES = \
	$(OWA_BROWSE3_EXE_MDB) \
	$(ATK_SHARP_DLL) \
	$(GLIB_SHARP_DLL) \
	$(GDK_SHARP_DLL) \
	$(GIO_SHARP_DLL) \
	$(CAIRO_SHARP_DLL) \
	$(PANGO_SHARP_DLL) \
	$(GTK_SHARP_DLL)  

BINARIES = \
	$(OWA_BROWSE3)  


RESGEN=resgen2

ATK_SHARP_DLL = $(BUILD_DIR)/atk-sharp.dll
GLIB_SHARP_DLL = $(BUILD_DIR)/glib-sharp.dll
GDK_SHARP_DLL = $(BUILD_DIR)/gdk-sharp.dll
GIO_SHARP_DLL = $(BUILD_DIR)/gio-sharp.dll
CAIRO_SHARP_DLL = $(BUILD_DIR)/cairo-sharp.dll
PANGO_SHARP_DLL = $(BUILD_DIR)/pango-sharp.dll
GTK_SHARP_DLL = $(BUILD_DIR)/gtk-sharp.dll
OWA_BROWSE3 = $(BUILD_DIR)/owa_browse3

FILES = \
	MainWindow.cs \
	Main.cs \
	AssemblyInfo.cs \
	../webkit-sharp3/WebKit.cs 

DATA_FILES = 

RESOURCES = 

EXTRAS = \
	app.desktop \
	owa_browse3.in 

REFERENCES =  \
	System \
	Mono.Posix \
	System.Web

DLL_REFERENCES =  \
	../../../../usr/lib/mono/gtk-sharp-3.0/atk-sharp.dll \
	../../../../usr/lib/mono/gtk-sharp-3.0/gdk-sharp.dll \
	../../../../usr/lib/mono/gtk-sharp-3.0/gtk-sharp.dll \
	../../../../usr/lib/mono/gtk-sharp-3.0/pango-sharp.dll \
	../../../../usr/lib/mono/gtk-sharp-3.0/glib-sharp.dll

CLEANFILES = $(PROGRAMFILES) $(BINARIES) 

#Targets
all-local: $(ASSEMBLY) $(PROGRAMFILES) $(BINARIES)  $(top_srcdir)/config.make



$(eval $(call emit-deploy-target,ATK_SHARP_DLL))
$(eval $(call emit-deploy-target,GLIB_SHARP_DLL))
$(eval $(call emit-deploy-target,GDK_SHARP_DLL))
$(eval $(call emit-deploy-target,GIO_SHARP_DLL))
$(eval $(call emit-deploy-target,CAIRO_SHARP_DLL))
$(eval $(call emit-deploy-target,PANGO_SHARP_DLL))
$(eval $(call emit-deploy-target,GTK_SHARP_DLL))
$(eval $(call emit-deploy-wrapper,OWA_BROWSE3,owa_browse3,x))


$(eval $(call emit_resgen_targets))
$(build_xamlg_list): %.xaml.g.cs: %.xaml
	xamlg '$<'


$(ASSEMBLY_MDB): $(ASSEMBLY)
$(ASSEMBLY): $(build_sources) $(build_resources) $(build_datafiles) $(DLL_REFERENCES) $(PROJECT_REFERENCES) $(build_xamlg_list) $(build_satellite_assembly_list)
	make pre-all-local-hook prefix=$(prefix)
	mkdir -p $(shell dirname $(ASSEMBLY))
	make $(CONFIG)_BeforeBuild
	$(ASSEMBLY_COMPILER_COMMAND) $(ASSEMBLY_COMPILER_FLAGS) -out:$(ASSEMBLY) -target:$(COMPILE_TARGET) $(build_sources_embed) $(build_resources_embed) $(build_references_ref)
	make $(CONFIG)_AfterBuild
	make post-all-local-hook prefix=$(prefix)

install-local: $(ASSEMBLY) $(ASSEMBLY_MDB)
	make pre-install-local-hook prefix=$(prefix)
	make install-satellite-assemblies prefix=$(prefix)
	mkdir -p '$(DESTDIR)$(libdir)/$(PACKAGE)'
	$(call cp,$(ASSEMBLY),$(DESTDIR)$(libdir)/$(PACKAGE))
	$(call cp,$(ASSEMBLY_MDB),$(DESTDIR)$(libdir)/$(PACKAGE))
	$(call cp,$(OWA_BROWSE3_EXE_MDB),$(DESTDIR)$(libdir)/$(PACKAGE))
	$(call cp,$(ATK_SHARP_DLL),$(DESTDIR)$(libdir)/$(PACKAGE))
	$(call cp,$(GLIB_SHARP_DLL),$(DESTDIR)$(libdir)/$(PACKAGE))
	$(call cp,$(GDK_SHARP_DLL),$(DESTDIR)$(libdir)/$(PACKAGE))
	$(call cp,$(GIO_SHARP_DLL),$(DESTDIR)$(libdir)/$(PACKAGE))
	$(call cp,$(CAIRO_SHARP_DLL),$(DESTDIR)$(libdir)/$(PACKAGE))
	$(call cp,$(PANGO_SHARP_DLL),$(DESTDIR)$(libdir)/$(PACKAGE))
	$(call cp,$(GTK_SHARP_DLL),$(DESTDIR)$(libdir)/$(PACKAGE))
	mkdir -p '$(DESTDIR)$(bindir)'
	$(call cp,$(OWA_BROWSE3),$(DESTDIR)$(bindir))
	make post-install-local-hook prefix=$(prefix)

uninstall-local: $(ASSEMBLY) $(ASSEMBLY_MDB)
	make pre-uninstall-local-hook prefix=$(prefix)
	make uninstall-satellite-assemblies prefix=$(prefix)
	$(call rm,$(ASSEMBLY),$(DESTDIR)$(libdir)/$(PACKAGE))
	$(call rm,$(ASSEMBLY_MDB),$(DESTDIR)$(libdir)/$(PACKAGE))
	$(call rm,$(OWA_BROWSE3_EXE_MDB),$(DESTDIR)$(libdir)/$(PACKAGE))
	$(call rm,$(ATK_SHARP_DLL),$(DESTDIR)$(libdir)/$(PACKAGE))
	$(call rm,$(GLIB_SHARP_DLL),$(DESTDIR)$(libdir)/$(PACKAGE))
	$(call rm,$(GDK_SHARP_DLL),$(DESTDIR)$(libdir)/$(PACKAGE))
	$(call rm,$(GIO_SHARP_DLL),$(DESTDIR)$(libdir)/$(PACKAGE))
	$(call rm,$(CAIRO_SHARP_DLL),$(DESTDIR)$(libdir)/$(PACKAGE))
	$(call rm,$(PANGO_SHARP_DLL),$(DESTDIR)$(libdir)/$(PACKAGE))
	$(call rm,$(GTK_SHARP_DLL),$(DESTDIR)$(libdir)/$(PACKAGE))
	$(call rm,$(OWA_BROWSE3),$(DESTDIR)$(bindir))
	make post-uninstall-local-hook prefix=$(prefix)

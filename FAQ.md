# Summary #

  * [General Questions](FAQ#General_Questions.md):
    1. [Why making an application like CDImageMerge?](FAQ#Why_making_an_application_like_CDImageMerge?.md)
    1. [Why using VB.NET?](FAQ#Why_using_VB.NET?.md)
    1. [How much time do you spend on improving the application?](FAQ#How_much_time_do_you_spend_on_improving_the_application?.md)
    1. [I'd like to collaborate with the project, what can I do?](FAQ#I'd_like_to_collaborate_with_the_project,_what_can_I_do?.md)
  * [CDImageMerge Questions](FAQ#CDImageMerge_Questions.md):
    1. [Which image formats does CDImageMerge support?](FAQ#Which_image_formats_does_CDImageMerge_support?.md)
    1. [Which CD-ROM formats does CDImageMerge support?](FAQ#Which_CD-ROM_formats_does_CDImageMerge_support?.md)
    1. [Does CDImageMerge support DVD images?](FAQ#Does_CDImageMerge_support_DVD_images?.md)
    1. [Does CDImageMerge support repairing damaged sectors?](FAQ#Does_CDImageMerge_support_repairing_damaged_sectors?.md)
    1. [Are multi-session CDs supported?](FAQ#Are_multi-session_CDs_supported?.md)
    1. [Which are your plans to improve CDImageMerge?](FAQ#Which_are_your_plans_to_improve_CDImageMerge?.md)
  * [DotNetIsoLib Questions](FAQ#Questions.md):
    1. [What is DotNetIsoLib?](FAQ#What_is_?.md)
    1. [I'm interested in using DotNetIsoLib, can I use it in my own application?](FAQ#I'm_interested_in_using_,_can_I_use_it_in_my_own_applicatio.md)
    1. [Can I write to an ISO file or create a new one with DotNetIsoLib?](FAQ#Can_I_write_to_an_ISO_file_or_create_a_new_one_with_?.md)
  * [Extensibility Questions](FAQ#Extensibility_Questions.md):
    1. [I'd like x feature added, is it possible?](FAQ#I'd_like_x_feature_added,_is_it_possible?.md)
    1. [Can I extend CDImageMerge functionality?](FAQ#Can_I_extend_CDImageMerge_functionality?.md)


# General Questions #

  * ## Why making an application like CDImageMerge? ##
> Actually, I used to collect PSX (Sony PlayStation) games back in the day, I like to keep ISOs of my CDs (in fact, I gave most of the games to a friend some time ago), however some of the purchased second hand games turned out to have scratches or some other minor damages that resulted in read errors, that forced me to use other CD/DVD drives to correctly read some of those sectors or get again the games from other sources. Seeing the problem, and without being able to find other applications that would help me in some situations in an optimal way, I decided to write my own application.

  * ## Why using VB.NET? ##
> This is just a matter of when and why I started the project: the company where I work mainly uses VB for the software it makes, as a result of this, when I start some quick experiment, or personal project I'm not expecting to share, I often use it as well. The project could be easily converted to C# if desired.

  * ## How much time do you spend on improving the application? ##
> I don't have too much spare time, so I only work on CDImageMerge for some minutes when I have a break and feel for it.

  * ## I'd like to collaborate with the project, what can I do? ##
> Get in contact with me, you can help with whatever is going to improve the project, be it new features, improve old ones, or add unit tests.


# CDImageMerge Questions #

  * ## Which image formats does CDImageMerge support? ##
> CDImageMerge only supports images in plain ISO format. As of August, 2011 there are only plans for adding support to parse .CUE and maybe .CCD files.

  * ## Which CD-ROM formats does CDImageMerge support? ##
> The application reads all images as Mode 2/2352. However, adding support for other formats is trivial and planned.

  * ## Does CDImageMerge support DVD images? ##
> DVD images normally use a completely different file system than the one supported. This may be looked and added in the future.

  * ## Does CDImageMerge support repairing damaged sectors? ##
> No, although it would a nice exercise to learn more, I think there are (there is?) already some good applications for this.

  * ## Are multi-session CDs supported? ##
> No, I haven't been able to test any. If I ever have the chance to do so, I'll be willing to add support for them.

  * ## Which are your plans to improve CDImageMerge? ##
> You can find some of the things I'm going to work on in this [PivotalTracker project page](https://www.pivotaltracker.com/projects/344619).


# DotNetIsoLib Questions #

  * ## What is DotNetIsoLib? ##
> It's the library I made for reading and parsing ISO images.

  * ## I'm interested in using DotNetIsoLib, can I use it in my own application? ##
> If DotNetIsoLib happens to be of use to you, you are free to use it on your own, I'd just like to hear what you are able to make with it.

  * ## Can I write to an ISO file or create a new one with DotNetIsoLib? ##
> No, the library doesn't support writing operations. They could be added tho, while developing the library, possible support for this was taken into account. If you are more interested on this do not hesitate to make a feature request.


# Extensibility Questions #

  * ## I'd like _x feature_ added, is it possible? ##
> Just create a feature request or drop me an e-mail and I'll see what can I do.

  * ## Can I extend CDImageMerge functionality? ##
> CDImageMerge exposes an ICdImageAction interface that can be implemented in your own class library to add your own features to the application. Once developed, just make sure your resulting assembly filename ends with ".CdImageAction.dll", and place it inside the CDImageMerge folder. Furthermore, CDImageMerge is fully open sourced, so if there is something you cannot easily add by using the built-in interface, you can dive into the code and modify whatever you want.
> If you make your own plugin, or modify something of the provided code I'd love to hear back from you.
# Proxy

**A class that functions as an interface to a particular resource. That resource may be remote, expensive to construct, or
may require logging or some other added functionality.**

## Motivation

- You are calling foo.Bar()
- This assumes that foo is in the same process as Bar()
- What if, later on, you want to put all Foo-related operations into a separate process
    - Can you avoid changing your code?
- Proxy to the rescue!
    - Same interface, entirely different behavior
- This is called a communication proxy
    - Other types: logging, virtual, guarding,...

## Summary

- A proxy has the same interface as the underflying object
- To create a proxy, simple replicate the existing interface of an object
- Add relevant functionality to the redefined member functions
- Different proxies (communication, logging, caching, etc.) have completely different behaviors

## Real world analogy

A credit card is a proxy for a bank account, which is a proxy for a bundle of cash. Both implement the same interface:
they can be used for making a payment.
A consumer feels great because there’s no need to carry loads of cash around.
A shop owner is also happy since the income from a transaction gets added electronically to the shop’s bank account
without the risk of losing the deposit or getting robbed on the way to the bank.

## Pseudocode

    // The interface of a remote service.
    interface ThirdPartyYouTubeLib is
    method listVideos()
    method getVideoInfo(id)
    method downloadVideo(id)
    // The concrete implementation of a service connector. Methods
    // of this class can request information from YouTube. The speed
    // of the request depends on a user's internet connection as
    // well as YouTube's. The application will slow down if a lot of
    // requests are fired at the same time, even if they all request
    // the same information.
    class ThirdPartyYouTubeClass implements ThirdPartyYouTubeLib is
    method listVideos() is
    // Send an API request to YouTube.
    method getVideoInfo(id) is
    // Get metadata about some video.
    method downloadVideo(id) is
    // Download a video file from YouTube.
    // To save some bandwidth, we can cache request results and keep
    // them for some time. But it may be impossible to put such code
    // directly into the service class. For example, it could have
    // been provided as part of a third party library and/or defined
    // as `final`. That's why we put the caching code into a new
    // proxy class which implements the same interface as the
    // service class. It delegates to the service object only when
    // the real requests have to be sent.
    class CachedYouTubeClass implements ThirdPartyYouTubeLib is
    private field service: ThirdPartyYouTubeLib
    private field listCache, videoCache
    field needReset
    constructor CachedYouTubeClass(service: ThirdPartyYouTubeLib) is
    this.service = service
    method listVideos() is
    if (listCache == null || needReset)
    listCache = service.listVideos()
    return listCache
    method getVideoInfo(id) is
    if (videoCache == null || needReset)
    videoCache = service.getVideoInfo(id)
    return videoCache
    method downloadVideo(id) is
    if (!downloadExists(id) || needReset)
    service.downloadVideo(id)
    // The GUI class, which used to work directly with a service
    // object, stays unchanged as long as it works with the service
    // object through an interface. We can safely pass a proxy
    // object instead of a real service object since they both
    // implement the same interface.
    class YouTubeManager is
    protected field service: ThirdPartyYouTubeLib
    constructor YouTubeManager(service: ThirdPartyYouTubeLib) is
    this.service = service
    method renderVideoPage(id) is
    info = service.getVideoInfo(id)
    // Render the video page.
    method renderListPanel() is
    list = service.listVideos()
    // Render the list of video thumbnails.
    method reactOnUserInput() is
    renderVideoPage()
    renderListPanel()
    // The application can configure proxies on the fly.
    class Application is
    method init() is
    aYouTubeService = new ThirdPartyYouTubeClass()
    aYouTubeProxy = new CachedYouTubeClass(aYouTubeService)
    manager = new YouTubeManager(aYouTubeProxy)
    manager.reactOnUserInput()
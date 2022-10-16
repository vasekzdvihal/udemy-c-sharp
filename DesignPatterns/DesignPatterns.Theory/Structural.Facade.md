# Façade

- *https://github.com/ActiveMesa/MdxConsole*
- *https://refactoring.guru/design-patterns/facade*

**Provides a simple, easy to understand/user interface over a large and sophisticated body of code**

### Motivation ###
- Balancing complexity and presentation/usability 
- Typical home
  - Many subsystems (e.g. electrical, plumbing, HVAC, etc.)
  - Complex internal structure (e.g. wiring, pipes, ducts, etc.)
  - End user is not exposed to internals
- Same with software!
  - Many systems working to provide flexibility, but...
  - API consumers want it to 'just work'

### Summary ###
- Build a Façade to provide a simplified API over a set of classes
- May with to (optionally) expose internals through the façade

### Real wold analogy ###
When you call a shop to place a phone order, an operator is you facade to all services and departments of the shop.
The operator provides you with simple voice interface to the ordering system, payment gateways, and various 
delivery services.

### Pseudocode ###

    // These are some of the classes of a complex 3rd-party video
    // conversion framework. We don't control that code, therefore
    // can't simplify it.
    class VideoFile
    // ...
    class OggCompressionCodec
    // ...
    class MPEG4CompressionCodec
    // ...
    class CodecFactory
    // ...
    class BitrateReader
    // ...
    class AudioMixer
    // ...
    // We create a facade class to hide the framework's complexity
    // behind a simple interface. It's a trade-off between
    // functionality and simplicity.
    class VideoConverter is
    method convert(filename, format):File is
    file = new VideoFile(filename)
    sourceCodec = new CodecFactory.extract(file)
    if (format == "mp4")
    destinationCodec = new MPEG4CompressionCodec()
    else
    destinationCodec = new OggCompressionCodec()
    buffer = BitrateReader.read(filename, sourceCodec)
    result = BitrateReader.convert(buffer, destinationCodec)
    result = (new AudioMixer()).fix(result)
    return new File(result)
    // Application classes don't depend on a billion classes
    // provided by the complex framework. Also, if you decide to
    // switch frameworks, you only need to rewrite the facade class.
    class Application is
    method main() is
    convertor = new VideoConverter()
    mp4 = convertor.convert("funny-cats-video.ogg", "mp4")
    mp4.save()
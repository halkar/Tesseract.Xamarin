using System;
using CoreGraphics;
using Foundation;
using ObjCRuntime;using UIKit;

namespace Tesseract.Binding.iOS
{
	// @protocol TesseractDelegate <NSObject>
	[Protocol, Model]
	[BaseType (typeof(NSObject))]
	interface TesseractDelegate
	{
		// @optional -(void)progressImageRecognitionForTesseract:(Tesseract *)tesseract;
		[Export ("progressImageRecognitionForTesseract:")]
		void ProgressImageRecognitionForTesseract (Tesseract tesseract);

		// @optional -(BOOL)shouldCancelImageRecognitionForTesseract:(Tesseract *)tesseract;
		[Export ("shouldCancelImageRecognitionForTesseract:")]
		bool ShouldCancelImageRecognitionForTesseract (Tesseract tesseract);
	}

	// @interface Tesseract : NSObject
	[BaseType (typeof(NSObject))]
	interface Tesseract
	{
		// +(NSString *)version;
		[Static]
		[Export ("version")]
//		[Verify (MethodToProperty)]
		string Version { get; }

		// @property (nonatomic, strong) NSString * language;
		[Export ("language", ArgumentSemantic.Strong)]
		string Language { get; set; }

		// @property (nonatomic, strong) UIImage * image;
		[Export ("image", ArgumentSemantic.Strong)]
		UIImage Image { get; set; }

		// @property (assign, nonatomic) CGRect rect;
		[Export ("rect", ArgumentSemantic.Assign)]
		CGRect Rect { get; set; }

		// @property (readonly, nonatomic) short progress;
		[Export ("progress")]
		short Progress { get; }

		// @property (readonly, nonatomic) NSString * recognizedText;
		[Export ("recognizedText")]
		string RecognizedText { get; }

		// @property (readonly, nonatomic) NSArray * characterBoxes;
		[Export ("characterBoxes")]
//		[Verify (StronglyTypedNSArray)]
		NSObject[] CharacterBoxes { get; }

		// @property (readonly, nonatomic) NSArray * getConfidenceByWord;
		[Export ("getConfidenceByWord")]
//		[Verify (StronglyTypedNSArray)]
		NSObject[] GetConfidenceByWord { get; }

		// @property (readonly, nonatomic) NSArray * getConfidenceBySymbol;
		[Export ("getConfidenceBySymbol")]
//		[Verify (StronglyTypedNSArray)]
		NSObject[] GetConfidenceBySymbol { get; }

		// @property (readonly, nonatomic) NSArray * getConfidenceByTextline;
		[Export ("getConfidenceByTextline")]
//		[Verify (StronglyTypedNSArray)]
		NSObject[] GetConfidenceByTextline { get; }

		// @property (readonly, nonatomic) NSArray * getConfidenceByParagraph;
		[Export ("getConfidenceByParagraph")]
//		[Verify (StronglyTypedNSArray)]
		NSObject[] GetConfidenceByParagraph { get; }

		// @property (readonly, nonatomic) NSArray * getConfidenceByBlock;
		[Export ("getConfidenceByBlock")]
//		[Verify (StronglyTypedNSArray)]
		NSObject[] GetConfidenceByBlock { get; }

		[Wrap ("WeakDelegate")]
		TesseractDelegate Delegate { get; set; }

		// @property (nonatomic, weak) id<TesseractDelegate> delegate;
		[NullAllowed, Export ("delegate", ArgumentSemantic.Weak)]
		NSObject WeakDelegate { get; set; }

		// -(id)initWithDataPath:(NSString *)dataPath language:(NSString *)language __attribute__((deprecated("")));
		[Export ("initWithDataPath:language:")]
		IntPtr Constructor (string dataPath, string language);

		// -(id)initWithLanguage:(NSString *)language;
		[Export ("initWithLanguage:")]
		IntPtr Constructor (string language);

		// -(void)setVariableValue:(NSString *)value forKey:(NSString *)key;
		[Export ("setVariableValue:forKey:")]
		void SetVariableValue (string value, string key);

		// -(BOOL)recognize;
		[Export ("recognize")]
//		[Verify (MethodToProperty)]
		bool Recognize ();

		// -(void)clear __attribute__((deprecated("")));
		[Export ("clear")]
		void Clear ();
	}
}

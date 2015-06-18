using System;
using CoreGraphics;
using Foundation;
using ObjCRuntime;
using UIKit;

namespace Tesseract.Binding.iOS
{
	partial interface Constants
	{
		// extern double TesseractOCRVersionNumber;
		[Field ("TesseractOCRVersionNumber")]
		double TesseractOCRVersionNumber { get; }

		// extern const unsigned char [] TesseractOCRVersionString;
		[Field ("TesseractOCRVersionString")]
		byte[] TesseractOCRVersionString { get; }
	}

	// @protocol G8TesseractDelegate <NSObject>
	[Protocol, Model]
	[BaseType (typeof(NSObject))]
	interface G8TesseractDelegate
	{
		// @optional -(void)progressImageRecognitionForTesseract:(G8Tesseract *)tesseract;
		[Export ("progressImageRecognitionForTesseract:")]
		void ProgressImageRecognitionForTesseract (G8Tesseract tesseract);

		// @optional -(BOOL)shouldCancelImageRecognitionForTesseract:(G8Tesseract *)tesseract;
		[Export ("shouldCancelImageRecognitionForTesseract:")]
		bool ShouldCancelImageRecognitionForTesseract (G8Tesseract tesseract);

		// @optional -(UIImage *)preprocessedImageForTesseract:(G8Tesseract *)tesseract sourceImage:(UIImage *)sourceImage;
		[Export ("preprocessedImageForTesseract:sourceImage:")]
		UIImage PreprocessedImageForTesseract (G8Tesseract tesseract, UIImage sourceImage);
	}

	partial interface Constants
	{
		// extern const NSInteger kG8DefaultResolution;
		[Field ("kG8DefaultResolution")]
		nint kG8DefaultResolution { get; }

		// extern const NSInteger kG8MinCredibleResolution;
		[Field ("kG8MinCredibleResolution")]
		nint kG8MinCredibleResolution { get; }

		// extern const NSInteger kG8MaxCredibleResolution;
		[Field ("kG8MaxCredibleResolution")]
		nint kG8MaxCredibleResolution { get; }
	}

	// @interface G8Tesseract : NSObject
	[BaseType (typeof(NSObject))]
	interface G8Tesseract
	{
		// +(NSString *)version;
		[Static]
		[Export ("version")]
		string Version { get; }

		// +(void)clearCache;
		[Static]
		[Export ("clearCache")]
		void ClearCache ();

		// @property (copy, nonatomic) NSString * language;
		[Export ("language")]
		string Language { get; set; }

		// @property (readonly, copy, nonatomic) NSString * absoluteDataPath;
		[Export ("absoluteDataPath")]
		string AbsoluteDataPath { get; }

		// @property (assign, nonatomic) G8OCREngineMode engineMode;
		[Export ("engineMode", ArgumentSemantic.Assign)]
		G8OCREngineMode EngineMode { get; set; }

		// @property (assign, nonatomic) G8PageSegmentationMode pageSegmentationMode;
		[Export ("pageSegmentationMode", ArgumentSemantic.Assign)]
		G8PageSegmentationMode PageSegmentationMode { get; set; }

		// @property (copy, nonatomic) NSString * charWhitelist;
		[Export ("charWhitelist")]
		string CharWhitelist { get; set; }

		// @property (copy, nonatomic) NSString * charBlacklist;
		[Export ("charBlacklist")]
		string CharBlacklist { get; set; }

		// @property (nonatomic, strong) UIImage * image;
		[Export ("image", ArgumentSemantic.Strong)]
		UIImage Image { get; set; }

		// @property (assign, nonatomic) CGRect rect;
		[Export ("rect", ArgumentSemantic.Assign)]
		CGRect Rect { get; set; }

		// @property (assign, nonatomic) NSInteger sourceResolution;
		[Export ("sourceResolution", ArgumentSemantic.Assign)]
		nint SourceResolution { get; set; }

		// @property (assign, nonatomic) NSTimeInterval maximumRecognitionTime;
		[Export ("maximumRecognitionTime")]
		double MaximumRecognitionTime { get; set; }

		// @property (readonly, nonatomic) NSUInteger progress;
		[Export ("progress")]
		nuint Progress { get; }

		// @property (readonly, nonatomic) NSString * recognizedText;
		[Export ("recognizedText")]
		string RecognizedText { get; }

		// -(NSString *)recognizedHOCRForPageNumber:(int)pageNumber;
		[Export ("recognizedHOCRForPageNumber:")]
		string RecognizedHOCRForPageNumber (int pageNumber);

		// -(void)analyseLayout;
		[Export ("analyseLayout")]
		void AnalyseLayout ();

		// @property (readonly, nonatomic) G8Orientation orientation;
		[Export ("orientation")]
		G8Orientation Orientation { get; }

		// @property (readonly, nonatomic) G8WritingDirection writingDirection;
		[Export ("writingDirection")]
		G8WritingDirection WritingDirection { get; }

		// @property (readonly, nonatomic) G8TextlineOrder textlineOrder;
		[Export ("textlineOrder")]
		G8TextlineOrder TextlineOrder { get; }

		// @property (readonly, nonatomic) CGFloat deskewAngle;
		[Export ("deskewAngle")]
		nfloat DeskewAngle { get; }

		// @property (readonly, nonatomic) NSArray * characterChoices;
		[Export ("characterChoices")]
		NSObject[] CharacterChoices { get; }

		// -(NSArray *)recognizedBlocksByIteratorLevel:(G8PageIteratorLevel)pageIteratorLevel;
		[Export ("recognizedBlocksByIteratorLevel:")]
		G8RecognizedBlock[] RecognizedBlocksByIteratorLevel (G8PageIteratorLevel pageIteratorLevel);

		// @property (readonly, nonatomic) UIImage * thresholdedImage;
		[Export ("thresholdedImage")]
		UIImage ThresholdedImage { get; }

		// -(UIImage *)imageWithBlocks:(NSArray *)blocks drawText:(BOOL)drawText thresholded:(BOOL)thresholded;
		[Export ("imageWithBlocks:drawText:thresholded:")]
		UIImage ImageWithBlocks (NSObject[] blocks, bool drawText, bool thresholded);

		[Wrap ("WeakDelegate")]
		G8TesseractDelegate Delegate { get; set; }

		// @property (nonatomic, weak) id<G8TesseractDelegate> delegate;
		[NullAllowed, Export ("delegate", ArgumentSemantic.Weak)]
		NSObject WeakDelegate { get; set; }

		// -(id)initWithLanguage:(NSString *)language;
		[Export ("initWithLanguage:")]
		IntPtr Constructor (string language);

		// -(id)initWithLanguage:(NSString *)language engineMode:(G8OCREngineMode)engineMode;
		[Export ("initWithLanguage:engineMode:")]
		IntPtr Constructor (string language, G8OCREngineMode engineMode);

		// -(id)initWithLanguage:(NSString *)language configDictionary:(NSDictionary *)configDictionary configFileNames:(NSArray *)configFileNames cachesRelatedDataPath:(NSString *)cachesRelatedDataPath engineMode:(G8OCREngineMode)engineMode;
		[Export ("initWithLanguage:configDictionary:configFileNames:cachesRelatedDataPath:engineMode:")]
		IntPtr Constructor (string language, NSDictionary configDictionary, NSObject[] configFileNames, string cachesRelatedDataPath, G8OCREngineMode engineMode);

		// -(id)initWithLanguage:(NSString *)language configDictionary:(NSDictionary *)configDictionary configFileNames:(NSArray *)configFileNames absoluteDataPath:(NSString *)absoluteDataPath engineMode:(G8OCREngineMode)engineMode copyFilesFromResources:(BOOL)copyFilesFromResources __attribute__((objc_designated_initializer));
		[Export ("initWithLanguage:configDictionary:configFileNames:absoluteDataPath:engineMode:copyFilesFromResources:")]
		IntPtr Constructor (string language, NSDictionary configDictionary, NSObject[] configFileNames, string absoluteDataPath, G8OCREngineMode engineMode, bool copyFilesFromResources);

		// -(void)setVariableValue:(NSString *)value forKey:(NSString *)key;
		[Export ("setVariableValue:forKey:")]
		void SetVariableValue (string value, string key);

		// -(NSString *)variableValueForKey:(NSString *)key;
		[Export ("variableValueForKey:")]
		string VariableValueForKey (string key);

		// -(void)setVariablesFromDictionary:(NSDictionary *)dictionary;
		[Export ("setVariablesFromDictionary:")]
		void SetVariablesFromDictionary (NSDictionary dictionary);

		// -(BOOL)recognize;
		[Export ("recognize")]
		bool Recognize();
	}

	// @interface G8RecognizedBlock : NSObject <NSCopying>
	[BaseType (typeof(NSObject))]
	interface G8RecognizedBlock : INSCopying
	{
		// @property (readonly, copy, nonatomic) NSString * text;
		[Export ("text")]
		string Text { get; }

		// @property (readonly, assign, nonatomic) CGRect boundingBox;
		[Export ("boundingBox", ArgumentSemantic.Assign)]
		CGRect BoundingBox { get; }

		// @property (readonly, assign, nonatomic) CGFloat confidence;
		[Export ("confidence", ArgumentSemantic.Assign)]
		nfloat Confidence { get; }

		// @property (readonly, assign, nonatomic) G8PageIteratorLevel level;
		[Export ("level", ArgumentSemantic.Assign)]
		G8PageIteratorLevel Level { get; }

		// -(instancetype)initWithText:(NSString *)text boundingBox:(CGRect)boundingBox confidence:(CGFloat)confidence level:(G8PageIteratorLevel)level;
		[Export ("initWithText:boundingBox:confidence:level:")]
		IntPtr Constructor (string text, CGRect boundingBox, nfloat confidence, G8PageIteratorLevel level);

		// -(CGRect)boundingBoxAtImageOfSize:(CGSize)imageSize;
		[Export ("boundingBoxAtImageOfSize:")]
		CGRect BoundingBoxAtImageOfSize (CGSize imageSize);
	}

	partial interface Constants
	{
		// extern NSString *const kG8ParamTospTableXhtSpRatio;
		[Field ("kG8ParamTospTableXhtSpRatio")]
		NSString kG8ParamTospTableXhtSpRatio { get; }

		// extern NSString *const kG8ParamTesseditTrainFromBoxes;
		[Field ("kG8ParamTesseditTrainFromBoxes")]
		NSString kG8ParamTesseditTrainFromBoxes { get; }

		// extern NSString *const kG8ParamTextordMinLinesize;
		[Field ("kG8ParamTextordMinLinesize")]
		NSString kG8ParamTextordMinLinesize { get; }

		// extern NSString *const kG8ParamTospWideFraction;
		[Field ("kG8ParamTospWideFraction")]
		NSString kG8ParamTospWideFraction { get; }

		// extern NSString *const kG8ParamTextordFixXheightBug;
		[Field ("kG8ParamTextordFixXheightBug")]
		NSString kG8ParamTextordFixXheightBug { get; }

		// extern NSString *const kG8ParamTesseditCertaintyThreshold;
		[Field ("kG8ParamTesseditCertaintyThreshold")]
		NSString kG8ParamTesseditCertaintyThreshold { get; }

		// extern NSString *const kG8ParamLanguageModelPenaltyIncrement;
		[Field ("kG8ParamLanguageModelPenaltyIncrement")]
		NSString kG8ParamLanguageModelPenaltyIncrement { get; }

		// extern NSString *const kG8ParamApplyboxLearnCharsAndCharFragsMode;
		[Field ("kG8ParamApplyboxLearnCharsAndCharFragsMode")]
		NSString kG8ParamApplyboxLearnCharsAndCharFragsMode { get; }

		// extern NSString *const kG8ParamTesseditMakeBoxesFromBoxes;
		[Field ("kG8ParamTesseditMakeBoxesFromBoxes")]
		NSString kG8ParamTesseditMakeBoxesFromBoxes { get; }

		// extern NSString *const kG8ParamTospPassWideFuzzSpToContext;
		[Field ("kG8ParamTospPassWideFuzzSpToContext")]
		NSString kG8ParamTospPassWideFuzzSpToContext { get; }

		// extern NSString *const kG8ParamClassifyUsePreAdaptedTemplates;
		[Field ("kG8ParamClassifyUsePreAdaptedTemplates")]
		NSString kG8ParamClassifyUsePreAdaptedTemplates { get; }

		// extern NSString *const kG8ParamChopOverlapKnob;
		[Field ("kG8ParamChopOverlapKnob")]
		NSString kG8ParamChopOverlapKnob { get; }

		// extern NSString *const kG8ParamRejAlphasInNumberPerm;
		[Field ("kG8ParamRejAlphasInNumberPerm")]
		NSString kG8ParamRejAlphasInNumberPerm { get; }

		// extern NSString *const kG8ParamTessBnMatching;
		[Field ("kG8ParamTessBnMatching")]
		NSString kG8ParamTessBnMatching { get; }

		// extern NSString *const kG8ParamTesseditUnrejAnyWd;
		[Field ("kG8ParamTesseditUnrejAnyWd")]
		NSString kG8ParamTesseditUnrejAnyWd { get; }

		// extern NSString *const kG8ParamDebugAcceptableWds;
		[Field ("kG8ParamDebugAcceptableWds")]
		NSString kG8ParamDebugAcceptableWds { get; }

		// extern NSString *const kG8ParamTospDebugLevel;
		[Field ("kG8ParamTospDebugLevel")]
		NSString kG8ParamTospDebugLevel { get; }

		// extern NSString *const kG8ParamFragmentsGuideChopper;
		[Field ("kG8ParamFragmentsGuideChopper")]
		NSString kG8ParamFragmentsGuideChopper { get; }

		// extern NSString *const kG8ParamTextordExpansionFactor;
		[Field ("kG8ParamTextordExpansionFactor")]
		NSString kG8ParamTextordExpansionFactor { get; }

		// extern NSString *const kG8ParamTesseditWriteRepCodes;
		[Field ("kG8ParamTesseditWriteRepCodes")]
		NSString kG8ParamTesseditWriteRepCodes { get; }

		// extern NSString *const kG8ParamCubeDebugLevel;
		[Field ("kG8ParamCubeDebugLevel")]
		NSString kG8ParamCubeDebugLevel { get; }

		// extern NSString *const kG8ParamLoadUnambigDawg;
		[Field ("kG8ParamLoadUnambigDawg")]
		NSString kG8ParamLoadUnambigDawg { get; }

		// extern NSString *const kG8ParamTesseditResegmentFromBoxes;
		[Field ("kG8ParamTesseditResegmentFromBoxes")]
		NSString kG8ParamTesseditResegmentFromBoxes { get; }

		// extern NSString *const kG8ParamMatcherPermanentClassesMin;
		[Field ("kG8ParamMatcherPermanentClassesMin")]
		NSString kG8ParamMatcherPermanentClassesMin { get; }

		// extern NSString *const kG8ParamCrunchDelRating;
		[Field ("kG8ParamCrunchDelRating")]
		NSString kG8ParamCrunchDelRating { get; }

		// extern NSString *const kG8ParamTospFlipFuzzKnToSp;
		[Field ("kG8ParamTospFlipFuzzKnToSp")]
		NSString kG8ParamTospFlipFuzzKnToSp { get; }

		// extern NSString *const kG8ParamSegmentPenaltyDictFrequentWord;
		[Field ("kG8ParamSegmentPenaltyDictFrequentWord")]
		NSString kG8ParamSegmentPenaltyDictFrequentWord { get; }

		// extern NSString *const kG8ParamLanguageModelNgramRatingFactor;
		[Field ("kG8ParamLanguageModelNgramRatingFactor")]
		NSString kG8ParamLanguageModelNgramRatingFactor { get; }

		// extern NSString *const kG8ParamTospRepSpace;
		[Field ("kG8ParamTospRepSpace")]
		NSString kG8ParamTospRepSpace { get; }

		// extern NSString *const kG8ParamTospEnoughSpaceSamplesForMedian;
		[Field ("kG8ParamTospEnoughSpaceSamplesForMedian")]
		NSString kG8ParamTospEnoughSpaceSamplesForMedian { get; }

		// extern NSString *const kG8ParamChopMinOutlinePoints;
		[Field ("kG8ParamChopMinOutlinePoints")]
		NSString kG8ParamChopMinOutlinePoints { get; }

		// extern NSString *const kG8ParamSpeckleLargeMaxSize;
		[Field ("kG8ParamSpeckleLargeMaxSize")]
		NSString kG8ParamSpeckleLargeMaxSize { get; }

		// extern NSString *const kG8ParamTesseditOcrEngineMode;
		[Field ("kG8ParamTesseditOcrEngineMode")]
		NSString kG8ParamTesseditOcrEngineMode { get; }

		// extern NSString *const kG8ParamTesseditCreateBoxfile;
		[Field ("kG8ParamTesseditCreateBoxfile")]
		NSString kG8ParamTesseditCreateBoxfile { get; }

		// extern NSString *const kG8ParamSuperscriptWorseCertainty;
		[Field ("kG8ParamSuperscriptWorseCertainty")]
		NSString kG8ParamSuperscriptWorseCertainty { get; }

		// extern NSString *const kG8ParamMaxViterbiListSize;
		[Field ("kG8ParamMaxViterbiListSize")]
		NSString kG8ParamMaxViterbiListSize { get; }

		// extern NSString *const kG8ParamChopGoodSplit;
		[Field ("kG8ParamChopGoodSplit")]
		NSString kG8ParamChopGoodSplit { get; }

		// extern NSString *const kG8ParamRejUseTessBlanks;
		[Field ("kG8ParamRejUseTessBlanks")]
		NSString kG8ParamRejUseTessBlanks { get; }

		// extern NSString *const kG8ParamTesseditCharBlacklist;
		[Field ("kG8ParamTesseditCharBlacklist")]
		NSString kG8ParamTesseditCharBlacklist { get; }

		// extern NSString *const kG8ParamTextordMinBlobsInRow;
		[Field ("kG8ParamTextordMinBlobsInRow")]
		NSString kG8ParamTextordMinBlobsInRow { get; }

		// extern NSString *const kG8ParamTextordTestY;
		[Field ("kG8ParamTextordTestY")]
		NSString kG8ParamTextordTestY { get; }

		// extern NSString *const kG8ParamTextordTestX;
		[Field ("kG8ParamTextordTestX")]
		NSString kG8ParamTextordTestX { get; }

		// extern NSString *const kG8ParamUserPatternsSuffix;
		[Field ("kG8ParamUserPatternsSuffix")]
		NSString kG8ParamUserPatternsSuffix { get; }

		// extern NSString *const kG8ParamTospUseXhtGaps;
		[Field ("kG8ParamTospUseXhtGaps")]
		NSString kG8ParamTospUseXhtGaps { get; }

		// extern NSString *const kG8ParamTesseditResegmentFromLineBoxes;
		[Field ("kG8ParamTesseditResegmentFromLineBoxes")]
		NSString kG8ParamTesseditResegmentFromLineBoxes { get; }

		// extern NSString *const kG8ParamUnlvTildeCrunching;
		[Field ("kG8ParamUnlvTildeCrunching")]
		NSString kG8ParamUnlvTildeCrunching { get; }

		// extern NSString *const kG8ParamSegsearchDebugLevel;
		[Field ("kG8ParamSegsearchDebugLevel")]
		NSString kG8ParamSegsearchDebugLevel { get; }

		// extern NSString *const kG8ParamLoadNumberDawg;
		[Field ("kG8ParamLoadNumberDawg")]
		NSString kG8ParamLoadNumberDawg { get; }

		// extern NSString *const kG8ParamDocDictCertaintyThreshold;
		[Field ("kG8ParamDocDictCertaintyThreshold")]
		NSString kG8ParamDocDictCertaintyThreshold { get; }

		// extern NSString *const kG8ParamTextordSplineMinblobs;
		[Field ("kG8ParamTextordSplineMinblobs")]
		NSString kG8ParamTextordSplineMinblobs { get; }

		// extern NSString *const kG8ParamCrunchPotPoorRate;
		[Field ("kG8ParamCrunchPotPoorRate")]
		NSString kG8ParamCrunchPotPoorRate { get; }

		// extern NSString *const kG8ParamTextordDebugXheights;
		[Field ("kG8ParamTextordDebugXheights")]
		NSString kG8ParamTextordDebugXheights { get; }

		// extern NSString *const kG8ParamSuspectLevel;
		[Field ("kG8ParamSuspectLevel")]
		NSString kG8ParamSuspectLevel { get; }

		// extern NSString *const kG8ParamCrunchPoorGarbageRate;
		[Field ("kG8ParamCrunchPoorGarbageRate")]
		NSString kG8ParamCrunchPoorGarbageRate { get; }

		// extern NSString *const kG8ParamTextordShowBlobs;
		[Field ("kG8ParamTextordShowBlobs")]
		NSString kG8ParamTextordShowBlobs { get; }

		// extern NSString *const kG8ParamTextordXheightErrorMargin;
		[Field ("kG8ParamTextordXheightErrorMargin")]
		NSString kG8ParamTextordXheightErrorMargin { get; }

		// extern NSString *const kG8ParamClassifySaveAdaptedTemplates;
		[Field ("kG8ParamClassifySaveAdaptedTemplates")]
		NSString kG8ParamClassifySaveAdaptedTemplates { get; }

		// extern NSString *const kG8ParamOkRepeatedChNonAlphanumWds;
		[Field ("kG8ParamOkRepeatedChNonAlphanumWds")]
		NSString kG8ParamOkRepeatedChNonAlphanumWds { get; }

		// extern NSString *const kG8ParamLanguageModelViterbiListMaxNumPrunable;
		[Field ("kG8ParamLanguageModelViterbiListMaxNumPrunable")]
		NSString kG8ParamLanguageModelViterbiListMaxNumPrunable { get; }

		// extern NSString *const kG8ParamCrunchLeaveUcStrings;
		[Field ("kG8ParamCrunchLeaveUcStrings")]
		NSString kG8ParamCrunchLeaveUcStrings { get; }

		// extern NSString *const kG8ParamLanguageModelNgramScaleFactor;
		[Field ("kG8ParamLanguageModelNgramScaleFactor")]
		NSString kG8ParamLanguageModelNgramScaleFactor { get; }

		// extern NSString *const kG8ParamTextordNoiseSxfract;
		[Field ("kG8ParamTextordNoiseSxfract")]
		NSString kG8ParamTextordNoiseSxfract { get; }

		// extern NSString *const kG8ParamChopSeamPileSize;
		[Field ("kG8ParamChopSeamPileSize")]
		NSString kG8ParamChopSeamPileSize { get; }

		// extern NSString *const kG8ParamTesseditAmbigsTraining;
		[Field ("kG8ParamTesseditAmbigsTraining")]
		NSString kG8ParamTesseditAmbigsTraining { get; }

		// extern NSString *const kG8ParamTospOnlyUsePropRows;
		[Field ("kG8ParamTospOnlyUsePropRows")]
		NSString kG8ParamTospOnlyUsePropRows { get; }

		// extern NSString *const kG8ParamParagraphDebugLevel;
		[Field ("kG8ParamParagraphDebugLevel")]
		NSString kG8ParamParagraphDebugLevel { get; }

		// extern NSString *const kG8ParamQualityOutlinePc;
		[Field ("kG8ParamQualityOutlinePc")]
		NSString kG8ParamQualityOutlinePc { get; }

		// extern NSString *const kG8ParamTessdataManagerDebugLevel;
		[Field ("kG8ParamTessdataManagerDebugLevel")]
		NSString kG8ParamTessdataManagerDebugLevel { get; }

		// extern NSString *const kG8ParamWordrecDebugBlamer;
		[Field ("kG8ParamWordrecDebugBlamer")]
		NSString kG8ParamWordrecDebugBlamer { get; }

		// extern NSString *const kG8ParamTesseditRejectMode;
		[Field ("kG8ParamTesseditRejectMode")]
		NSString kG8ParamTesseditRejectMode { get; }

		// extern NSString *const kG8ParamCrunchTerribleRating;
		[Field ("kG8ParamCrunchTerribleRating")]
		NSString kG8ParamCrunchTerribleRating { get; }

		// extern NSString *const kG8ParamStopperNondictCertaintyBase;
		[Field ("kG8ParamStopperNondictCertaintyBase")]
		NSString kG8ParamStopperNondictCertaintyBase { get; }

		// extern NSString *const kG8ParamSegmentPenaltyDictCaseBad;
		[Field ("kG8ParamSegmentPenaltyDictCaseBad")]
		NSString kG8ParamSegmentPenaltyDictCaseBad { get; }

		// extern NSString *const kG8ParamTesseditRowRejGoodDocs;
		[Field ("kG8ParamTesseditRowRejGoodDocs")]
		NSString kG8ParamTesseditRowRejGoodDocs { get; }

		// extern NSString *const kG8ParamTextordShowFinalBlobs;
		[Field ("kG8ParamTextordShowFinalBlobs")]
		NSString kG8ParamTextordShowFinalBlobs { get; }

		// extern NSString *const kG8ParamLanguageModelNgramNonmatchScore;
		[Field ("kG8ParamLanguageModelNgramNonmatchScore")]
		NSString kG8ParamLanguageModelNgramNonmatchScore { get; }

		// extern NSString *const kG8ParamLanguageModelDebugLevel;
		[Field ("kG8ParamLanguageModelDebugLevel")]
		NSString kG8ParamLanguageModelDebugLevel { get; }

		// extern NSString *const kG8ParamQualityRowrejPc;
		[Field ("kG8ParamQualityRowrejPc")]
		NSString kG8ParamQualityRowrejPc { get; }

		// extern NSString *const kG8ParamSegsearchMaxFutileClassifications;
		[Field ("kG8ParamSegsearchMaxFutileClassifications")]
		NSString kG8ParamSegsearchMaxFutileClassifications { get; }

		// extern NSString *const kG8ParamTesseditTrainingTess;
		[Field ("kG8ParamTesseditTrainingTess")]
		NSString kG8ParamTesseditTrainingTess { get; }

		// extern NSString *const kG8ParamCrunchDelLowWord;
		[Field ("kG8ParamCrunchDelLowWord")]
		NSString kG8ParamCrunchDelLowWord { get; }

		// extern NSString *const kG8ParamLanguageModelPenaltyScript;
		[Field ("kG8ParamLanguageModelPenaltyScript")]
		NSString kG8ParamLanguageModelPenaltyScript { get; }

		// extern NSString *const kG8ParamClassifyCharacterFragmentsGarbageCertaintyThreshold;
		[Field ("kG8ParamClassifyCharacterFragmentsGarbageCertaintyThreshold")]
		NSString kG8ParamClassifyCharacterFragmentsGarbageCertaintyThreshold { get; }

		// extern NSString *const kG8ParamClassifyCpCutoffStrength;
		[Field ("kG8ParamClassifyCpCutoffStrength")]
		NSString kG8ParamClassifyCpCutoffStrength { get; }

		// extern NSString *const kG8ParamTesseditPreserveBlkRejPerfectWds;
		[Field ("kG8ParamTesseditPreserveBlkRejPerfectWds")]
		NSString kG8ParamTesseditPreserveBlkRejPerfectWds { get; }

		// extern NSString *const kG8ParamTesseditUpperFlipHyphen;
		[Field ("kG8ParamTesseditUpperFlipHyphen")]
		NSString kG8ParamTesseditUpperFlipHyphen { get; }

		// extern NSString *const kG8ParamChopOkSplit;
		[Field ("kG8ParamChopOkSplit")]
		NSString kG8ParamChopOkSplit { get; }

		// extern NSString *const kG8ParamTextordSkewsmoothOffset;
		[Field ("kG8ParamTextordSkewsmoothOffset")]
		NSString kG8ParamTextordSkewsmoothOffset { get; }

		// extern NSString *const kG8ParamXheightPenaltyInconsistent;
		[Field ("kG8ParamXheightPenaltyInconsistent")]
		NSString kG8ParamXheightPenaltyInconsistent { get; }

		// extern NSString *const kG8ParamChopDebug;
		[Field ("kG8ParamChopDebug")]
		NSString kG8ParamChopDebug { get; }

		// extern NSString *const kG8ParamTextordNoiseRejwords;
		[Field ("kG8ParamTextordNoiseRejwords")]
		NSString kG8ParamTextordNoiseRejwords { get; }

		// extern NSString *const kG8ParamFragmentsDebug;
		[Field ("kG8ParamFragmentsDebug")]
		NSString kG8ParamFragmentsDebug { get; }

		// extern NSString *const kG8ParamTextordNoiseRowratio;
		[Field ("kG8ParamTextordNoiseRowratio")]
		NSString kG8ParamTextordNoiseRowratio { get; }

		// extern NSString *const kG8ParamChopXYWeight;
		[Field ("kG8ParamChopXYWeight")]
		NSString kG8ParamChopXYWeight { get; }

		// extern NSString *const kG8ParamXHtAcceptanceTolerance;
		[Field ("kG8ParamXHtAcceptanceTolerance")]
		NSString kG8ParamXHtAcceptanceTolerance { get; }

		// extern NSString *const kG8ParamMaxPermuterAttempts;
		[Field ("kG8ParamMaxPermuterAttempts")]
		NSString kG8ParamMaxPermuterAttempts { get; }

		// extern NSString *const kG8ParamLanguageModelUseSigmoidalCertainty;
		[Field ("kG8ParamLanguageModelUseSigmoidalCertainty")]
		NSString kG8ParamLanguageModelUseSigmoidalCertainty { get; }

		// extern NSString *const kG8ParamTextordStraightBaselines;
		[Field ("kG8ParamTextordStraightBaselines")]
		NSString kG8ParamTextordStraightBaselines { get; }

		// extern NSString *const kG8ParamDebugXHtLevel;
		[Field ("kG8ParamDebugXHtLevel")]
		NSString kG8ParamDebugXHtLevel { get; }

		// extern NSString *const kG8ParamCrunchLeaveAcceptStrings;
		[Field ("kG8ParamCrunchLeaveAcceptStrings")]
		NSString kG8ParamCrunchLeaveAcceptStrings { get; }

		// extern NSString *const kG8ParamTesseditAdaptionDebug;
		[Field ("kG8ParamTesseditAdaptionDebug")]
		NSString kG8ParamTesseditAdaptionDebug { get; }

		// extern NSString *const kG8ParamTesseditDebugQualityMetrics;
		[Field ("kG8ParamTesseditDebugQualityMetrics")]
		NSString kG8ParamTesseditDebugQualityMetrics { get; }

		// extern NSString *const kG8ParamTospBlockUseCertSpaces;
		[Field ("kG8ParamTospBlockUseCertSpaces")]
		NSString kG8ParamTospBlockUseCertSpaces { get; }

		// extern NSString *const kG8ParamTesseditMinimalRejection;
		[Field ("kG8ParamTesseditMinimalRejection")]
		NSString kG8ParamTesseditMinimalRejection { get; }

		// extern NSString *const kG8ParamCrunchDelMinHt;
		[Field ("kG8ParamCrunchDelMinHt")]
		NSString kG8ParamCrunchDelMinHt { get; }

		// extern NSString *const kG8ParamTospGapFactor;
		[Field ("kG8ParamTospGapFactor")]
		NSString kG8ParamTospGapFactor { get; }

		// extern NSString *const kG8ParamTextordFixMakerowBug;
		[Field ("kG8ParamTextordFixMakerowBug")]
		NSString kG8ParamTextordFixMakerowBug { get; }

		// extern NSString *const kG8ParamWordrecSkipNoTruthWords;
		[Field ("kG8ParamWordrecSkipNoTruthWords")]
		NSString kG8ParamWordrecSkipNoTruthWords { get; }

		// extern NSString *const kG8ParamTextordNoiseHfract;
		[Field ("kG8ParamTextordNoiseHfract")]
		NSString kG8ParamTextordNoiseHfract { get; }

		// extern NSString *const kG8ParamTextordNoiseSyfract;
		[Field ("kG8ParamTextordNoiseSyfract")]
		NSString kG8ParamTextordNoiseSyfract { get; }

		// extern NSString *const kG8ParamLanguageModelPenaltySpacing;
		[Field ("kG8ParamLanguageModelPenaltySpacing")]
		NSString kG8ParamLanguageModelPenaltySpacing { get; }

		// extern NSString *const kG8ParamSuperscriptDebug;
		[Field ("kG8ParamSuperscriptDebug")]
		NSString kG8ParamSuperscriptDebug { get; }

		// extern NSString *const kG8ParamWordrecNoBlock;
		[Field ("kG8ParamWordrecNoBlock")]
		NSString kG8ParamWordrecNoBlock { get; }

		// extern NSString *const kG8ParamTextordSkewLag;
		[Field ("kG8ParamTextordSkewLag")]
		NSString kG8ParamTextordSkewLag { get; }

		// extern NSString *const kG8ParamChopVerticalCreep;
		[Field ("kG8ParamChopVerticalCreep")]
		NSString kG8ParamChopVerticalCreep { get; }

		// extern NSString *const kG8ParamSuspectSpaceLevel;
		[Field ("kG8ParamSuspectSpaceLevel")]
		NSString kG8ParamSuspectSpaceLevel { get; }

		// extern NSString *const kG8ParamClassifyMinNormScaleX;
		[Field ("kG8ParamClassifyMinNormScaleX")]
		NSString kG8ParamClassifyMinNormScaleX { get; }

		// extern NSString *const kG8ParamTesseditCreatePdf;
		[Field ("kG8ParamTesseditCreatePdf")]
		NSString kG8ParamTesseditCreatePdf { get; }

		// extern NSString *const kG8ParamLanguageModelPenaltyFont;
		[Field ("kG8ParamLanguageModelPenaltyFont")]
		NSString kG8ParamLanguageModelPenaltyFont { get; }

		// extern NSString *const kG8ParamApplyboxPage;
		[Field ("kG8ParamApplyboxPage")]
		NSString kG8ParamApplyboxPage { get; }

		// extern NSString *const kG8ParamClassifyDebugLevel;
		[Field ("kG8ParamClassifyDebugLevel")]
		NSString kG8ParamClassifyDebugLevel { get; }

		// extern NSString *const kG8ParamUseOnlyFirstUft8Step;
		[Field ("kG8ParamUseOnlyFirstUft8Step")]
		NSString kG8ParamUseOnlyFirstUft8Step { get; }

		// extern NSString *const kG8ParamTesseditGoodDocStillRowrejWd;
		[Field ("kG8ParamTesseditGoodDocStillRowrejWd")]
		NSString kG8ParamTesseditGoodDocStillRowrejWd { get; }

		// extern NSString *const kG8ParamRejTrustDocDawg;
		[Field ("kG8ParamRejTrustDocDawg")]
		NSString kG8ParamRejTrustDocDawg { get; }

		// extern NSString *const kG8ParamTospRowUseCertSpaces1;
		[Field ("kG8ParamTospRowUseCertSpaces1")]
		NSString kG8ParamTospRowUseCertSpaces1 { get; }

		// extern NSString *const kG8ParamTospRedoKernLimit;
		[Field ("kG8ParamTospRedoKernLimit")]
		NSString kG8ParamTospRedoKernLimit { get; }

		// extern NSString *const kG8ParamCrunchDelMaxHt;
		[Field ("kG8ParamCrunchDelMaxHt")]
		NSString kG8ParamCrunchDelMaxHt { get; }

		// extern NSString *const kG8ParamClassifyMaxRatingRatio;
		[Field ("kG8ParamClassifyMaxRatingRatio")]
		NSString kG8ParamClassifyMaxRatingRatio { get; }

		// extern NSString *const kG8ParamTextordAscxRatioMin;
		[Field ("kG8ParamTextordAscxRatioMin")]
		NSString kG8ParamTextordAscxRatioMin { get; }

		// extern NSString *const kG8ParamUnrecognisedChar;
		[Field ("kG8ParamUnrecognisedChar")]
		NSString kG8ParamUnrecognisedChar { get; }

		// extern NSString *const kG8ParamCrunchDelMinWidth;
		[Field ("kG8ParamCrunchDelMinWidth")]
		NSString kG8ParamCrunchDelMinWidth { get; }

		// extern NSString *const kG8ParamTospTableKnSpRatio;
		[Field ("kG8ParamTospTableKnSpRatio")]
		NSString kG8ParamTospTableKnSpRatio { get; }

		// extern NSString *const kG8ParamTextordAscheightModeFraction;
		[Field ("kG8ParamTextordAscheightModeFraction")]
		NSString kG8ParamTextordAscheightModeFraction { get; }

		// extern NSString *const kG8ParamSuperscriptScaledownRatio;
		[Field ("kG8ParamSuperscriptScaledownRatio")]
		NSString kG8ParamSuperscriptScaledownRatio { get; }

		// extern NSString *const kG8ParamTospWideAspectRatio;
		[Field ("kG8ParamTospWideAspectRatio")]
		NSString kG8ParamTospWideAspectRatio { get; }

		// extern NSString *const kG8ParamTospMinSaneKnSp;
		[Field ("kG8ParamTospMinSaneKnSp")]
		NSString kG8ParamTospMinSaneKnSp { get; }

		// extern NSString *const kG8ParamTospFuzzySpaceFactor;
		[Field ("kG8ParamTospFuzzySpaceFactor")]
		NSString kG8ParamTospFuzzySpaceFactor { get; }

		// extern NSString *const kG8ParamTesseditClassMissScale;
		[Field ("kG8ParamTesseditClassMissScale")]
		NSString kG8ParamTesseditClassMissScale { get; }

		// extern NSString *const kG8ParamCrunchDelCert;
		[Field ("kG8ParamCrunchDelCert")]
		NSString kG8ParamCrunchDelCert { get; }

		// extern NSString *const kG8ParamWordToDebugLengths;
		[Field ("kG8ParamWordToDebugLengths")]
		NSString kG8ParamWordToDebugLengths { get; }

		// extern NSString *const kG8ParamTextordDescheightModeFraction;
		[Field ("kG8ParamTextordDescheightModeFraction")]
		NSString kG8ParamTextordDescheightModeFraction { get; }

		// extern NSString *const kG8ParamTesseditWriteImages;
		[Field ("kG8ParamTesseditWriteImages")]
		NSString kG8ParamTesseditWriteImages { get; }

		// extern NSString *const kG8ParamSuspectRatingPerCh;
		[Field ("kG8ParamSuspectRatingPerCh")]
		NSString kG8ParamSuspectRatingPerCh { get; }

		// extern NSString *const kG8ParamTesseditParallelize;
		[Field ("kG8ParamTesseditParallelize")]
		NSString kG8ParamTesseditParallelize { get; }

		// extern NSString *const kG8ParamStopperCertaintyPerChar;
		[Field ("kG8ParamStopperCertaintyPerChar")]
		NSString kG8ParamStopperCertaintyPerChar { get; }

		// extern NSString *const kG8ParamTestPt;
		[Field ("kG8ParamTestPt")]
		NSString kG8ParamTestPt { get; }

		// extern NSString *const kG8ParamLanguageModelNgramSpaceDelimitedLanguage;
		[Field ("kG8ParamLanguageModelNgramSpaceDelimitedLanguage")]
		NSString kG8ParamLanguageModelNgramSpaceDelimitedLanguage { get; }

		// extern NSString *const kG8ParamCrunchRatingMax;
		[Field ("kG8ParamCrunchRatingMax")]
		NSString kG8ParamCrunchRatingMax { get; }

		// extern NSString *const kG8ParamLanguageModelPenaltyCase;
		[Field ("kG8ParamLanguageModelPenaltyCase")]
		NSString kG8ParamLanguageModelPenaltyCase { get; }

		// extern NSString *const kG8ParamTospEnoughSmallGaps;
		[Field ("kG8ParamTospEnoughSmallGaps")]
		NSString kG8ParamTospEnoughSmallGaps { get; }

		// extern NSString *const kG8ParamSuspectAcceptRating;
		[Field ("kG8ParamSuspectAcceptRating")]
		NSString kG8ParamSuspectAcceptRating { get; }

		// extern NSString *const kG8ParamClassifyMaxCertaintyMargin;
		[Field ("kG8ParamClassifyMaxCertaintyMargin")]
		NSString kG8ParamClassifyMaxCertaintyMargin { get; }

		// extern NSString *const kG8ParamClassifyClassPrunerMultiplier;
		[Field ("kG8ParamClassifyClassPrunerMultiplier")]
		NSString kG8ParamClassifyClassPrunerMultiplier { get; }

		// extern NSString *const kG8ParamRejUseGoodPerm;
		[Field ("kG8ParamRejUseGoodPerm")]
		NSString kG8ParamRejUseGoodPerm { get; }

		// extern NSString *const kG8ParamIl1AdaptionTest;
		[Field ("kG8ParamIl1AdaptionTest")]
		NSString kG8ParamIl1AdaptionTest { get; }

		// extern NSString *const kG8ParamXHtMinChange;
		[Field ("kG8ParamXHtMinChange")]
		NSString kG8ParamXHtMinChange { get; }

		// extern NSString *const kG8ParamCrunchPotGarbage;
		[Field ("kG8ParamCrunchPotGarbage")]
		NSString kG8ParamCrunchPotGarbage { get; }

		// extern NSString *const kG8ParamTesseditTruncateWordchoiceLog;
		[Field ("kG8ParamTesseditTruncateWordchoiceLog")]
		NSString kG8ParamTesseditTruncateWordchoiceLog { get; }

		// extern NSString *const kG8ParamClassifyEnableAdaptiveMatcher;
		[Field ("kG8ParamClassifyEnableAdaptiveMatcher")]
		NSString kG8ParamClassifyEnableAdaptiveMatcher { get; }

		// extern NSString *const kG8ParamTesseditCreateHocr;
		[Field ("kG8ParamTesseditCreateHocr")]
		NSString kG8ParamTesseditCreateHocr { get; }

		// extern NSString *const kG8ParamCertaintyScale;
		[Field ("kG8ParamCertaintyScale")]
		NSString kG8ParamCertaintyScale { get; }

		// extern NSString *const kG8ParamStopperSmallwordSize;
		[Field ("kG8ParamStopperSmallwordSize")]
		NSString kG8ParamStopperSmallwordSize { get; }

		// extern NSString *const kG8ParamWordrecMaxJoinChunks;
		[Field ("kG8ParamWordrecMaxJoinChunks")]
		NSString kG8ParamWordrecMaxJoinChunks { get; }

		// extern NSString *const kG8ParamTextordNoiseNormratio;
		[Field ("kG8ParamTextordNoiseNormratio")]
		NSString kG8ParamTextordNoiseNormratio { get; }

		// extern NSString *const kG8ParamChopSplitDistKnob;
		[Field ("kG8ParamChopSplitDistKnob")]
		NSString kG8ParamChopSplitDistKnob { get; }

		// extern NSString *const kG8ParamHyphenDebugLevel;
		[Field ("kG8ParamHyphenDebugLevel")]
		NSString kG8ParamHyphenDebugLevel { get; }

		// extern NSString *const kG8ParamTesseditWriteUnlv;
		[Field ("kG8ParamTesseditWriteUnlv")]
		NSString kG8ParamTesseditWriteUnlv { get; }

		// extern NSString *const kG8ParamQualityBlobPc;
		[Field ("kG8ParamQualityBlobPc")]
		NSString kG8ParamQualityBlobPc { get; }

		// extern NSString *const kG8ParamTextordNoiseSncount;
		[Field ("kG8ParamTextordNoiseSncount")]
		NSString kG8ParamTextordNoiseSncount { get; }

		// extern NSString *const kG8ParamTospFuzzySpaceFactor1;
		[Field ("kG8ParamTospFuzzySpaceFactor1")]
		NSString kG8ParamTospFuzzySpaceFactor1 { get; }

		// extern NSString *const kG8ParamTospFuzzySpaceFactor2;
		[Field ("kG8ParamTospFuzzySpaceFactor2")]
		NSString kG8ParamTospFuzzySpaceFactor2 { get; }

		// extern NSString *const kG8ParamInteractiveDisplayMode;
		[Field ("kG8ParamInteractiveDisplayMode")]
		NSString kG8ParamInteractiveDisplayMode { get; }

		// extern NSString *const kG8ParamTextordNoiseSizefraction;
		[Field ("kG8ParamTextordNoiseSizefraction")]
		NSString kG8ParamTextordNoiseSizefraction { get; }

		// extern NSString *const kG8ParamTesseditWriteBlockSeparators;
		[Field ("kG8ParamTesseditWriteBlockSeparators")]
		NSString kG8ParamTesseditWriteBlockSeparators { get; }

		// extern NSString *const kG8ParamTesseditTestAdaptionMode;
		[Field ("kG8ParamTesseditTestAdaptionMode")]
		NSString kG8ParamTesseditTestAdaptionMode { get; }

		// extern NSString *const kG8ParamPolyAllowDetailedFx;
		[Field ("kG8ParamPolyAllowDetailedFx")]
		NSString kG8ParamPolyAllowDetailedFx { get; }

		// extern NSString *const kG8ParamTospUsePreChopping;
		[Field ("kG8ParamTospUsePreChopping")]
		NSString kG8ParamTospUsePreChopping { get; }

		// extern NSString *const kG8ParamTospNarrowAspectRatio;
		[Field ("kG8ParamTospNarrowAspectRatio")]
		NSString kG8ParamTospNarrowAspectRatio { get; }

		// extern NSString *const kG8ParamTextordShowParallelRows;
		[Field ("kG8ParamTextordShowParallelRows")]
		NSString kG8ParamTextordShowParallelRows { get; }

		// extern NSString *const kG8ParamTextordBlobSizeSmallile;
		[Field ("kG8ParamTextordBlobSizeSmallile")]
		NSString kG8ParamTextordBlobSizeSmallile { get; }

		// extern NSString *const kG8ParamCrunchDebug;
		[Field ("kG8ParamCrunchDebug")]
		NSString kG8ParamCrunchDebug { get; }

		// extern NSString *const kG8ParamClassifyEnableAdaptiveDebugger;
		[Field ("kG8ParamClassifyEnableAdaptiveDebugger")]
		NSString kG8ParamClassifyEnableAdaptiveDebugger { get; }

		// extern NSString *const kG8ParamCrunchLongRepetitions;
		[Field ("kG8ParamCrunchLongRepetitions")]
		NSString kG8ParamCrunchLongRepetitions { get; }

		// extern NSString *const kG8ParamLanguageModelPenaltyChartype;
		[Field ("kG8ParamLanguageModelPenaltyChartype")]
		NSString kG8ParamLanguageModelPenaltyChartype { get; }

		// extern NSString *const kG8ParamMatcherPerfectThreshold;
		[Field ("kG8ParamMatcherPerfectThreshold")]
		NSString kG8ParamMatcherPerfectThreshold { get; }

		// extern NSString *const kG8ParamLanguageModelPenaltyNonFreqDictWord;
		[Field ("kG8ParamLanguageModelPenaltyNonFreqDictWord")]
		NSString kG8ParamLanguageModelPenaltyNonFreqDictWord { get; }

		// extern NSString *const kG8ParamTextordHeavyNr;
		[Field ("kG8ParamTextordHeavyNr")]
		NSString kG8ParamTextordHeavyNr { get; }

		// extern NSString *const kG8ParamTospThresholdBias2;
		[Field ("kG8ParamTospThresholdBias2")]
		NSString kG8ParamTospThresholdBias2 { get; }

		// extern NSString *const kG8ParamTospThresholdBias1;
		[Field ("kG8ParamTospThresholdBias1")]
		NSString kG8ParamTospThresholdBias1 { get; }

		// extern NSString *const kG8ParamTextordParallelBaselines;
		[Field ("kG8ParamTextordParallelBaselines")]
		NSString kG8ParamTextordParallelBaselines { get; }

		// extern NSString *const kG8ParamTesseditRejectRowPercent;
		[Field ("kG8ParamTesseditRejectRowPercent")]
		NSString kG8ParamTesseditRejectRowPercent { get; }

		// extern NSString *const kG8ParamClassifyAdaptFeatureThreshold;
		[Field ("kG8ParamClassifyAdaptFeatureThreshold")]
		NSString kG8ParamClassifyAdaptFeatureThreshold { get; }

		// extern NSString *const kG8ParamChopWidthChangeKnob;
		[Field ("kG8ParamChopWidthChangeKnob")]
		NSString kG8ParamChopWidthChangeKnob { get; }

		// extern NSString *const kG8ParamTesseditZeroKelvinRejection;
		[Field ("kG8ParamTesseditZeroKelvinRejection")]
		NSString kG8ParamTesseditZeroKelvinRejection { get; }

		// extern NSString *const kG8ParamTextordMaxNoiseSize;
		[Field ("kG8ParamTextordMaxNoiseSize")]
		NSString kG8ParamTextordMaxNoiseSize { get; }

		// extern NSString *const kG8ParamTospFlipCaution;
		[Field ("kG8ParamTospFlipCaution")]
		NSString kG8ParamTospFlipCaution { get; }

		// extern NSString *const kG8ParamTestPtX;
		[Field ("kG8ParamTestPtX")]
		NSString kG8ParamTestPtX { get; }

		// extern NSString *const kG8ParamTesseditGoodQualityUnrej;
		[Field ("kG8ParamTesseditGoodQualityUnrej")]
		NSString kG8ParamTesseditGoodQualityUnrej { get; }

		// extern NSString *const kG8ParamClassifyLearningDebugLevel;
		[Field ("kG8ParamClassifyLearningDebugLevel")]
		NSString kG8ParamClassifyLearningDebugLevel { get; }

		// extern NSString *const kG8ParamTospIgnoreBigGaps;
		[Field ("kG8ParamTospIgnoreBigGaps")]
		NSString kG8ParamTospIgnoreBigGaps { get; }

		// extern NSString *const kG8ParamLoadSystemDawg;
		[Field ("kG8ParamLoadSystemDawg")]
		NSString kG8ParamLoadSystemDawg { get; }

		// extern NSString *const kG8ParamTospNarrowBlobsNotCert;
		[Field ("kG8ParamTospNarrowBlobsNotCert")]
		NSString kG8ParamTospNarrowBlobsNotCert { get; }

		// extern NSString *const kG8ParamLanguageModelNgramOn;
		[Field ("kG8ParamLanguageModelNgramOn")]
		NSString kG8ParamLanguageModelNgramOn { get; }

		// extern NSString *const kG8ParamWordrecRunBlamer;
		[Field ("kG8ParamWordrecRunBlamer")]
		NSString kG8ParamWordrecRunBlamer { get; }

		// extern NSString *const kG8ParamTospTableFuzzyKnSpRatio;
		[Field ("kG8ParamTospTableFuzzyKnSpRatio")]
		NSString kG8ParamTospTableFuzzyKnSpRatio { get; }

		// extern NSString *const kG8ParamTextordNoRejects;
		[Field ("kG8ParamTextordNoRejects")]
		NSString kG8ParamTextordNoRejects { get; }

		// extern NSString *const kG8ParamTesseditImageBorder;
		[Field ("kG8ParamTesseditImageBorder")]
		NSString kG8ParamTesseditImageBorder { get; }

		// extern NSString *const kG8ParamTospAllFlipsFuzzy;
		[Field ("kG8ParamTospAllFlipsFuzzy")]
		NSString kG8ParamTospAllFlipsFuzzy { get; }

		// extern NSString *const kG8ParamDebugFixSpaceLevel;
		[Field ("kG8ParamDebugFixSpaceLevel")]
		NSString kG8ParamDebugFixSpaceLevel { get; }

		// extern NSString *const kG8ParamRej1IlUseDictWord;
		[Field ("kG8ParamRej1IlUseDictWord")]
		NSString kG8ParamRej1IlUseDictWord { get; }

		// extern NSString *const kG8ParamTextordNoiseDebug;
		[Field ("kG8ParamTextordNoiseDebug")]
		NSString kG8ParamTextordNoiseDebug { get; }

		// extern NSString *const kG8ParamSegmentNonalphabeticScript;
		[Field ("kG8ParamSegmentNonalphabeticScript")]
		NSString kG8ParamSegmentNonalphabeticScript { get; }

		// extern NSString *const kG8ParamTospOldToMethod;
		[Field ("kG8ParamTospOldToMethod")]
		NSString kG8ParamTospOldToMethod { get; }

		// extern NSString *const kG8ParamTesseditTimingDebug;
		[Field ("kG8ParamTesseditTimingDebug")]
		NSString kG8ParamTesseditTimingDebug { get; }

		// extern NSString *const kG8ParamPrioritizeDivision;
		[Field ("kG8ParamPrioritizeDivision")]
		NSString kG8ParamPrioritizeDivision { get; }

		// extern NSString *const kG8ParamTesseditEnableDocDict;
		[Field ("kG8ParamTesseditEnableDocDict")]
		NSString kG8ParamTesseditEnableDocDict { get; }

		// extern NSString *const kG8ParamCrunchPotPoorCert;
		[Field ("kG8ParamCrunchPotPoorCert")]
		NSString kG8ParamCrunchPotPoorCert { get; }

		// extern NSString *const kG8ParamSegmentPenaltyDictNonword;
		[Field ("kG8ParamSegmentPenaltyDictNonword")]
		NSString kG8ParamSegmentPenaltyDictNonword { get; }

		// extern NSString *const kG8ParamCrunchPoorGarbageCert;
		[Field ("kG8ParamCrunchPoorGarbageCert")]
		NSString kG8ParamCrunchPoorGarbageCert { get; }

		// extern NSString *const kG8ParamTextordOccupancyThreshold;
		[Field ("kG8ParamTextordOccupancyThreshold")]
		NSString kG8ParamTextordOccupancyThreshold { get; }

		// extern NSString *const kG8ParamTextordSingleHeightMode;
		[Field ("kG8ParamTextordSingleHeightMode")]
		NSString kG8ParamTextordSingleHeightMode { get; }

		// extern NSString *const kG8ParamCrunchPotIndicators;
		[Field ("kG8ParamCrunchPotIndicators")]
		NSString kG8ParamCrunchPotIndicators { get; }

		// extern NSString *const kG8ParamTextordBlshiftXfraction;
		[Field ("kG8ParamTextordBlshiftXfraction")]
		NSString kG8ParamTextordBlshiftXfraction { get; }

		// extern NSString *const kG8ParamTospOldToConstrainSpKn;
		[Field ("kG8ParamTospOldToConstrainSpKn")]
		NSString kG8ParamTospOldToConstrainSpKn { get; }

		// extern NSString *const kG8ParamRatingScale;
		[Field ("kG8ParamRatingScale")]
		NSString kG8ParamRatingScale { get; }

		// extern NSString *const kG8ParamTextordSplineShiftFraction;
		[Field ("kG8ParamTextordSplineShiftFraction")]
		NSString kG8ParamTextordSplineShiftFraction { get; }

		// extern NSString *const kG8ParamTextordDescxRatioMax;
		[Field ("kG8ParamTextordDescxRatioMax")]
		NSString kG8ParamTextordDescxRatioMax { get; }

		// extern NSString *const kG8ParamTesseditConsistentReps;
		[Field ("kG8ParamTesseditConsistentReps")]
		NSString kG8ParamTesseditConsistentReps { get; }

		// extern NSString *const kG8ParamTospSillyKnSpGap;
		[Field ("kG8ParamTospSillyKnSpGap")]
		NSString kG8ParamTospSillyKnSpGap { get; }

		// extern NSString *const kG8ParamLanguageModelPenaltyNonDictWord;
		[Field ("kG8ParamLanguageModelPenaltyNonDictWord")]
		NSString kG8ParamLanguageModelPenaltyNonDictWord { get; }

		// extern NSString *const kG8ParamTextordChopWidth;
		[Field ("kG8ParamTextordChopWidth")]
		NSString kG8ParamTextordChopWidth { get; }

		// extern NSString *const kG8ParamTextordOverlapX;
		[Field ("kG8ParamTextordOverlapX")]
		NSString kG8ParamTextordOverlapX { get; }

		// extern NSString *const kG8ParamTesseditWordForWord;
		[Field ("kG8ParamTesseditWordForWord")]
		NSString kG8ParamTesseditWordForWord { get; }

		// extern NSString *const kG8ParamSegmentAdjustDebug;
		[Field ("kG8ParamSegmentAdjustDebug")]
		NSString kG8ParamSegmentAdjustDebug { get; }

		// extern NSString *const kG8ParamTextordXheightModeFraction;
		[Field ("kG8ParamTextordXheightModeFraction")]
		NSString kG8ParamTextordXheightModeFraction { get; }

		// extern NSString *const kG8ParamTesseditUsePrimaryParamsModel;
		[Field ("kG8ParamTesseditUsePrimaryParamsModel")]
		NSString kG8ParamTesseditUsePrimaryParamsModel { get; }

		// extern NSString *const kG8ParamFixspNonNoiseLimit;
		[Field ("kG8ParamFixspNonNoiseLimit")]
		NSString kG8ParamFixspNonNoiseLimit { get; }

		// extern NSString *const kG8ParamSuperscriptMinYBottom;
		[Field ("kG8ParamSuperscriptMinYBottom")]
		NSString kG8ParamSuperscriptMinYBottom { get; }

		// extern NSString *const kG8ParamLanguageModelViterbiListMaxSize;
		[Field ("kG8ParamLanguageModelViterbiListMaxSize")]
		NSString kG8ParamLanguageModelViterbiListMaxSize { get; }

		// extern NSString *const kG8ParamTextordWidthLimit;
		[Field ("kG8ParamTextordWidthLimit")]
		NSString kG8ParamTextordWidthLimit { get; }

		// extern NSString *const kG8ParamTextordSplineOutlierFraction;
		[Field ("kG8ParamTextordSplineOutlierFraction")]
		NSString kG8ParamTextordSplineOutlierFraction { get; }

		// extern NSString *const kG8ParamTospStatsUseXhtGaps;
		[Field ("kG8ParamTospStatsUseXhtGaps")]
		NSString kG8ParamTospStatsUseXhtGaps { get; }

		// extern NSString *const kG8ParamClassifyNormMethod;
		[Field ("kG8ParamClassifyNormMethod")]
		NSString kG8ParamClassifyNormMethod { get; }

		// extern NSString *const kG8ParamMinOrientationMargin;
		[Field ("kG8ParamMinOrientationMargin")]
		NSString kG8ParamMinOrientationMargin { get; }

		// extern NSString *const kG8ParamMatcherGreatThreshold;
		[Field ("kG8ParamMatcherGreatThreshold")]
		NSString kG8ParamMatcherGreatThreshold { get; }

		// extern NSString *const kG8ParamTesseditInitConfigOnly;
		[Field ("kG8ParamTesseditInitConfigOnly")]
		NSString kG8ParamTesseditInitConfigOnly { get; }

		// extern NSString *const kG8ParamTextordShowExpandedRows;
		[Field ("kG8ParamTextordShowExpandedRows")]
		NSString kG8ParamTextordShowExpandedRows { get; }

		// extern NSString *const kG8ParamTesseditPreserveRowRejPerfectWds;
		[Field ("kG8ParamTesseditPreserveRowRejPerfectWds")]
		NSString kG8ParamTesseditPreserveRowRejPerfectWds { get; }

		// extern NSString *const kG8ParamMatcherRatingMargin;
		[Field ("kG8ParamMatcherRatingMargin")]
		NSString kG8ParamMatcherRatingMargin { get; }

		// extern NSString *const kG8ParamDisableCharacterFragments;
		[Field ("kG8ParamDisableCharacterFragments")]
		NSString kG8ParamDisableCharacterFragments { get; }

		// extern NSString *const kG8ParamSubscriptMaxYTop;
		[Field ("kG8ParamSubscriptMaxYTop")]
		NSString kG8ParamSubscriptMaxYTop { get; }

		// extern NSString *const kG8ParamMergeFragmentsInMatrix;
		[Field ("kG8ParamMergeFragmentsInMatrix")]
		NSString kG8ParamMergeFragmentsInMatrix { get; }

		// extern NSString *const kG8ParamBlandUnrej;
		[Field ("kG8ParamBlandUnrej")]
		NSString kG8ParamBlandUnrej { get; }

		// extern NSString *const kG8ParamCrunchDelHighWord;
		[Field ("kG8ParamCrunchDelHighWord")]
		NSString kG8ParamCrunchDelHighWord { get; }

		// extern NSString *const kG8ParamTextordMinxh;
		[Field ("kG8ParamTextordMinxh")]
		NSString kG8ParamTextordMinxh { get; }

		// extern NSString *const kG8ParamTospOldSpKnThFactor;
		[Field ("kG8ParamTospOldSpKnThFactor")]
		NSString kG8ParamTospOldSpKnThFactor { get; }

		// extern NSString *const kG8ParamLanguageModelNgramSmallProb;
		[Field ("kG8ParamLanguageModelNgramSmallProb")]
		NSString kG8ParamLanguageModelNgramSmallProb { get; }

		// extern NSString *const kG8ParamOutlinesOdd;
		[Field ("kG8ParamOutlinesOdd")]
		NSString kG8ParamOutlinesOdd { get; }

		// extern NSString *const kG8ParamTesseditMatcherLog;
		[Field ("kG8ParamTesseditMatcherLog")]
		NSString kG8ParamTesseditMatcherLog { get; }

		// extern NSString *const kG8ParamStopperDebugLevel;
		[Field ("kG8ParamStopperDebugLevel")]
		NSString kG8ParamStopperDebugLevel { get; }

		// extern NSString *const kG8ParamTesseditRejectBadQualWds;
		[Field ("kG8ParamTesseditRejectBadQualWds")]
		NSString kG8ParamTesseditRejectBadQualWds { get; }

		// extern NSString *const kG8ParamNumericPunctuation;
		[Field ("kG8ParamNumericPunctuation")]
		NSString kG8ParamNumericPunctuation { get; }

		// extern NSString *const kG8ParamChopSharpnessKnob;
		[Field ("kG8ParamChopSharpnessKnob")]
		NSString kG8ParamChopSharpnessKnob { get; }

		// extern NSString *const kG8ParamConflictSetIL1;
		[Field ("kG8ParamConflictSetIL1")]
		NSString kG8ParamConflictSetIL1 { get; }

		// extern NSString *const kG8ParamClassifyIntegerMatcherMultiplier;
		[Field ("kG8ParamClassifyIntegerMatcherMultiplier")]
		NSString kG8ParamClassifyIntegerMatcherMultiplier { get; }

		// extern NSString *const kG8ParamSuspectShortWords;
		[Field ("kG8ParamSuspectShortWords")]
		NSString kG8ParamSuspectShortWords { get; }

		// extern NSString *const kG8ParamClassifyBlnNumericMode;
		[Field ("kG8ParamClassifyBlnNumericMode")]
		NSString kG8ParamClassifyBlnNumericMode { get; }

		// extern NSString *const kG8ParamTesseditRejectBlockPercent;
		[Field ("kG8ParamTesseditRejectBlockPercent")]
		NSString kG8ParamTesseditRejectBlockPercent { get; }

		// extern NSString *const kG8ParamClassifyNonlinearNorm;
		[Field ("kG8ParamClassifyNonlinearNorm")]
		NSString kG8ParamClassifyNonlinearNorm { get; }

		// extern NSString *const kG8ParamTospRowUseCertSpaces;
		[Field ("kG8ParamTospRowUseCertSpaces")]
		NSString kG8ParamTospRowUseCertSpaces { get; }

		// extern NSString *const kG8ParamRejUseTessAccepted;
		[Field ("kG8ParamRejUseTessAccepted")]
		NSString kG8ParamRejUseTessAccepted { get; }

		// extern NSString *const kG8ParamMatcherClusteringMaxAngleDelta;
		[Field ("kG8ParamMatcherClusteringMaxAngleDelta")]
		NSString kG8ParamMatcherClusteringMaxAngleDelta { get; }

		// extern NSString *const kG8ParamClassifyCharNormRange;
		[Field ("kG8ParamClassifyCharNormRange")]
		NSString kG8ParamClassifyCharNormRange { get; }

		// extern NSString *const kG8ParamParagraphTextBased;
		[Field ("kG8ParamParagraphTextBased")]
		NSString kG8ParamParagraphTextBased { get; }

		// extern NSString *const kG8ParamChopCenterKnob;
		[Field ("kG8ParamChopCenterKnob")]
		NSString kG8ParamChopCenterKnob { get; }

		// extern NSString *const kG8ParamTextordNewInitialXheight;
		[Field ("kG8ParamTextordNewInitialXheight")]
		NSString kG8ParamTextordNewInitialXheight { get; }

		// extern NSString *const kG8ParamTextordNoiseSizelimit;
		[Field ("kG8ParamTextordNoiseSizelimit")]
		NSString kG8ParamTextordNoiseSizelimit { get; }

		// extern NSString *const kG8ParamClassifyMisfitJunkPenalty;
		[Field ("kG8ParamClassifyMisfitJunkPenalty")]
		NSString kG8ParamClassifyMisfitJunkPenalty { get; }

		// extern NSString *const kG8ParamClassifyLearnDebugStr;
		[Field ("kG8ParamClassifyLearnDebugStr")]
		NSString kG8ParamClassifyLearnDebugStr { get; }

		// extern NSString *const kG8ParamTesseditMinimalRejPass1;
		[Field ("kG8ParamTesseditMinimalRejPass1")]
		NSString kG8ParamTesseditMinimalRejPass1 { get; }

		// extern NSString *const kG8ParamCrunchIncludeNumerals;
		[Field ("kG8ParamCrunchIncludeNumerals")]
		NSString kG8ParamCrunchIncludeNumerals { get; }

		// extern NSString *const kG8ParamTextordInitialascIle;
		[Field ("kG8ParamTextordInitialascIle")]
		NSString kG8ParamTextordInitialascIle { get; }

		// extern NSString *const kG8ParamFileType;
		[Field ("kG8ParamFileType")]
		NSString kG8ParamFileType { get; }

		// extern NSString *const kG8ParamTesseditDontBlkrejGoodWds;
		[Field ("kG8ParamTesseditDontBlkrejGoodWds")]
		NSString kG8ParamTesseditDontBlkrejGoodWds { get; }

		// extern NSString *const kG8ParamTesseditTestAdaption;
		[Field ("kG8ParamTesseditTestAdaption")]
		NSString kG8ParamTesseditTestAdaption { get; }

		// extern NSString *const kG8ParamTesseditRejectionDebug;
		[Field ("kG8ParamTesseditRejectionDebug")]
		NSString kG8ParamTesseditRejectionDebug { get; }

		// extern NSString *const kG8ParamTospFuzzyKnFraction;
		[Field ("kG8ParamTospFuzzyKnFraction")]
		NSString kG8ParamTospFuzzyKnFraction { get; }

		// extern NSString *const kG8ParamTesseditUseRejectSpaces;
		[Field ("kG8ParamTesseditUseRejectSpaces")]
		NSString kG8ParamTesseditUseRejectSpaces { get; }

		// extern NSString *const kG8ParamClassifyEnableLearning;
		[Field ("kG8ParamClassifyEnableLearning")]
		NSString kG8ParamClassifyEnableLearning { get; }

		// extern NSString *const kG8ParamClassifyMinNormScaleY;
		[Field ("kG8ParamClassifyMinNormScaleY")]
		NSString kG8ParamClassifyMinNormScaleY { get; }

		// extern NSString *const kG8ParamMatcherDebugFlags;
		[Field ("kG8ParamMatcherDebugFlags")]
		NSString kG8ParamMatcherDebugFlags { get; }

		// extern NSString *const kG8ParamSaveAltChoices;
		[Field ("kG8ParamSaveAltChoices")]
		NSString kG8ParamSaveAltChoices { get; }

		// extern NSString *const kG8ParamCrunchAcceptOk;
		[Field ("kG8ParamCrunchAcceptOk")]
		NSString kG8ParamCrunchAcceptOk { get; }

		// extern NSString *const kG8ParamTextordNoiseAreaRatio;
		[Field ("kG8ParamTextordNoiseAreaRatio")]
		NSString kG8ParamTextordNoiseAreaRatio { get; }

		// extern NSString *const kG8ParamTesseditCharWhitelist;
		[Field ("kG8ParamTesseditCharWhitelist")]
		NSString kG8ParamTesseditCharWhitelist { get; }

		// extern NSString *const kG8ParamCrunchLeaveOkStrings;
		[Field ("kG8ParamCrunchLeaveOkStrings")]
		NSString kG8ParamCrunchLeaveOkStrings { get; }

		// extern NSString *const kG8ParamTospIgnoreVeryBigGaps;
		[Field ("kG8ParamTospIgnoreVeryBigGaps")]
		NSString kG8ParamTospIgnoreVeryBigGaps { get; }

		// extern NSString *const kG8ParamQualityCharPc;
		[Field ("kG8ParamQualityCharPc")]
		NSString kG8ParamQualityCharPc { get; }

		// extern NSString *const kG8ParamTesseditEnableBigramCorrection;
		[Field ("kG8ParamTesseditEnableBigramCorrection")]
		NSString kG8ParamTesseditEnableBigramCorrection { get; }

		// extern NSString *const kG8ParamSpeckleRatingPenalty;
		[Field ("kG8ParamSpeckleRatingPenalty")]
		NSString kG8ParamSpeckleRatingPenalty { get; }

		// extern NSString *const kG8ParamTextordShowBoxes;
		[Field ("kG8ParamTextordShowBoxes")]
		NSString kG8ParamTextordShowBoxes { get; }

		// extern NSString *const kG8ParamSegmentPenaltyDictCaseOk;
		[Field ("kG8ParamSegmentPenaltyDictCaseOk")]
		NSString kG8ParamSegmentPenaltyDictCaseOk { get; }

		// extern NSString *const kG8ParamTesseditDebugDocRejection;
		[Field ("kG8ParamTesseditDebugDocRejection")]
		NSString kG8ParamTesseditDebugDocRejection { get; }

		// extern NSString *const kG8ParamTesseditWholeWdRejRowPercent;
		[Field ("kG8ParamTesseditWholeWdRejRowPercent")]
		NSString kG8ParamTesseditWholeWdRejRowPercent { get; }

		// extern NSString *const kG8ParamClassifyClassPrunerThreshold;
		[Field ("kG8ParamClassifyClassPrunerThreshold")]
		NSString kG8ParamClassifyClassPrunerThreshold { get; }

		// extern NSString *const kG8ParamTesseditFixFuzzySpaces;
		[Field ("kG8ParamTesseditFixFuzzySpaces")]
		NSString kG8ParamTesseditFixFuzzySpaces { get; }

		// extern NSString *const kG8ParamTesseditFlip0O;
		[Field ("kG8ParamTesseditFlip0O")]
		NSString kG8ParamTesseditFlip0O { get; }

		// extern NSString *const kG8ParamChopMinOutlineArea;
		[Field ("kG8ParamChopMinOutlineArea")]
		NSString kG8ParamChopMinOutlineArea { get; }

		// extern NSString *const kG8ParamTesseditZeroRejection;
		[Field ("kG8ParamTesseditZeroRejection")]
		NSString kG8ParamTesseditZeroRejection { get; }

		// extern NSString *const kG8ParamTesseditOverridePermuter;
		[Field ("kG8ParamTesseditOverridePermuter")]
		NSString kG8ParamTesseditOverridePermuter { get; }

		// extern NSString *const kG8ParamTospSanityMethod;
		[Field ("kG8ParamTospSanityMethod")]
		NSString kG8ParamTospSanityMethod { get; }

		// extern NSString *const kG8ParamTospFuzzySpFraction;
		[Field ("kG8ParamTospFuzzySpFraction")]
		NSString kG8ParamTospFuzzySpFraction { get; }

		// extern NSString *const kG8ParamSaveRawChoices;
		[Field ("kG8ParamSaveRawChoices")]
		NSString kG8ParamSaveRawChoices { get; }

		// extern NSString *const kG8ParamMatcherAvgNoiseSize;
		[Field ("kG8ParamMatcherAvgNoiseSize")]
		NSString kG8ParamMatcherAvgNoiseSize { get; }

		// extern NSString *const kG8ParamQualityMinInitialAlphasReqd;
		[Field ("kG8ParamQualityMinInitialAlphasReqd")]
		NSString kG8ParamQualityMinInitialAlphasReqd { get; }

		// extern NSString *const kG8ParamTospMaxSaneKnThresh;
		[Field ("kG8ParamTospMaxSaneKnThresh")]
		NSString kG8ParamTospMaxSaneKnThresh { get; }

		// extern NSString *const kG8ParamMatcherGoodThreshold;
		[Field ("kG8ParamMatcherGoodThreshold")]
		NSString kG8ParamMatcherGoodThreshold { get; }

		// extern NSString *const kG8ParamWordToDebug;
		[Field ("kG8ParamWordToDebug")]
		NSString kG8ParamWordToDebug { get; }

		// extern NSString *const kG8ParamUserWordsSuffix;
		[Field ("kG8ParamUserWordsSuffix")]
		NSString kG8ParamUserWordsSuffix { get; }

		// extern NSString *const kG8ParamTospRecoveryIsolatedRowStats;
		[Field ("kG8ParamTospRecoveryIsolatedRowStats")]
		NSString kG8ParamTospRecoveryIsolatedRowStats { get; }

		// extern NSString *const kG8ParamRejUseSensibleWd;
		[Field ("kG8ParamRejUseSensibleWd")]
		NSString kG8ParamRejUseSensibleWd { get; }

		// extern NSString *const kG8ParamWordrecEnableAssoc;
		[Field ("kG8ParamWordrecEnableAssoc")]
		NSString kG8ParamWordrecEnableAssoc { get; }

		// extern NSString *const kG8ParamTesseditSingleMatch;
		[Field ("kG8ParamTesseditSingleMatch")]
		NSString kG8ParamTesseditSingleMatch { get; }

		// extern NSString *const kG8ParamChopCenteredMaxwidth;
		[Field ("kG8ParamChopCenteredMaxwidth")]
		NSString kG8ParamChopCenteredMaxwidth { get; }

		// extern NSString *const kG8ParamLoadFreqDawg;
		[Field ("kG8ParamLoadFreqDawg")]
		NSString kG8ParamLoadFreqDawg { get; }

		// extern NSString *const kG8ParamTextordSkewIle;
		[Field ("kG8ParamTextordSkewIle")]
		NSString kG8ParamTextordSkewIle { get; }

		// extern NSString *const kG8ParamSegmentPenaltyNgramBestChoice;
		[Field ("kG8ParamSegmentPenaltyNgramBestChoice")]
		NSString kG8ParamSegmentPenaltyNgramBestChoice { get; }

		// extern NSString *const kG8ParamTextordDescxRatioMin;
		[Field ("kG8ParamTextordDescxRatioMin")]
		NSString kG8ParamTextordDescxRatioMin { get; }

		// extern NSString *const kG8ParamSegmentPenaltyGarbage;
		[Field ("kG8ParamSegmentPenaltyGarbage")]
		NSString kG8ParamSegmentPenaltyGarbage { get; }

		// extern NSString *const kG8ParamSaveDocWords;
		[Field ("kG8ParamSaveDocWords")]
		NSString kG8ParamSaveDocWords { get; }

		// extern NSString *const kG8ParamChopSplitLength;
		[Field ("kG8ParamChopSplitLength")]
		NSString kG8ParamChopSplitLength { get; }

		// extern NSString *const kG8ParamTesseditWriteParamsToFile;
		[Field ("kG8ParamTesseditWriteParamsToFile")]
		NSString kG8ParamTesseditWriteParamsToFile { get; }

		// extern NSString *const kG8ParamTextordOldXheight;
		[Field ("kG8ParamTextordOldXheight")]
		NSString kG8ParamTextordOldXheight { get; }

		// extern NSString *const kG8ParamClassifyAdaptProtoThreshold;
		[Field ("kG8ParamClassifyAdaptProtoThreshold")]
		NSString kG8ParamClassifyAdaptProtoThreshold { get; }

		// extern NSString *const kG8ParamTextordTabfindShowVlines;
		[Field ("kG8ParamTextordTabfindShowVlines")]
		NSString kG8ParamTextordTabfindShowVlines { get; }

		// extern NSString *const kG8ParamMatcherDebugSeparateWindows;
		[Field ("kG8ParamMatcherDebugSeparateWindows")]
		NSString kG8ParamMatcherDebugSeparateWindows { get; }

		// extern NSString *const kG8ParamTessCnMatching;
		[Field ("kG8ParamTessCnMatching")]
		NSString kG8ParamTessCnMatching { get; }

		// extern NSString *const kG8ParamTextordSplineMedianwin;
		[Field ("kG8ParamTextordSplineMedianwin")]
		NSString kG8ParamTextordSplineMedianwin { get; }

		// extern NSString *const kG8ParamTospFlipFuzzSpToKn;
		[Field ("kG8ParamTospFlipFuzzSpToKn")]
		NSString kG8ParamTospFlipFuzzSpToKn { get; }

		// extern NSString *const kG8ParamStopperNoAcceptableChoices;
		[Field ("kG8ParamStopperNoAcceptableChoices")]
		NSString kG8ParamStopperNoAcceptableChoices { get; }

		// extern NSString *const kG8ParamWordrecWorstState;
		[Field ("kG8ParamWordrecWorstState")]
		NSString kG8ParamWordrecWorstState { get; }

		// extern NSString *const kG8ParamTextordOldBaselines;
		[Field ("kG8ParamTextordOldBaselines")]
		NSString kG8ParamTextordOldBaselines { get; }

		// extern NSString *const kG8ParamTextordNoiseTranslimit;
		[Field ("kG8ParamTextordNoiseTranslimit")]
		NSString kG8ParamTextordNoiseTranslimit { get; }

		// extern NSString *const kG8ParamTesseditDebugFonts;
		[Field ("kG8ParamTesseditDebugFonts")]
		NSString kG8ParamTesseditDebugFonts { get; }

		// extern NSString *const kG8ParamTextordNoiseRejrows;
		[Field ("kG8ParamTextordNoiseRejrows")]
		NSString kG8ParamTextordNoiseRejrows { get; }

		// extern NSString *const kG8ParamTextordUseCjkFpModel;
		[Field ("kG8ParamTextordUseCjkFpModel")]
		NSString kG8ParamTextordUseCjkFpModel { get; }

		// extern NSString *const kG8ParamLanguageModelNgramOrder;
		[Field ("kG8ParamLanguageModelNgramOrder")]
		NSString kG8ParamLanguageModelNgramOrder { get; }

		// extern NSString *const kG8ParamCrunchLeaveLcStrings;
		[Field ("kG8ParamCrunchLeaveLcStrings")]
		NSString kG8ParamCrunchLeaveLcStrings { get; }

		// extern NSString *const kG8ParamClassifyAdaptedPruningThreshold;
		[Field ("kG8ParamClassifyAdaptedPruningThreshold")]
		NSString kG8ParamClassifyAdaptedPruningThreshold { get; }

		// extern NSString *const kG8ParamFixspSmallOutlinesSize;
		[Field ("kG8ParamFixspSmallOutlinesSize")]
		NSString kG8ParamFixspSmallOutlinesSize { get; }

		// extern NSString *const kG8ParamWordrecDebugLevel;
		[Field ("kG8ParamWordrecDebugLevel")]
		NSString kG8ParamWordrecDebugLevel { get; }

		// extern NSString *const kG8ParamTesseditPagesegMode;
		[Field ("kG8ParamTesseditPagesegMode")]
		NSString kG8ParamTesseditPagesegMode { get; }

		// extern NSString *const kG8ParamChsTrailingPunct1;
		[Field ("kG8ParamChsTrailingPunct1")]
		NSString kG8ParamChsTrailingPunct1 { get; }

		// extern NSString *const kG8ParamLanguageModelMinCompoundLength;
		[Field ("kG8ParamLanguageModelMinCompoundLength")]
		NSString kG8ParamLanguageModelMinCompoundLength { get; }

		// extern NSString *const kG8ParamLoadPuncDawg;
		[Field ("kG8ParamLoadPuncDawg")]
		NSString kG8ParamLoadPuncDawg { get; }

		// extern NSString *const kG8ParamTospForceWordbreakOnPunct;
		[Field ("kG8ParamTospForceWordbreakOnPunct")]
		NSString kG8ParamTospForceWordbreakOnPunct { get; }

		// extern NSString *const kG8ParamTospDontFoolWithSmallKerns;
		[Field ("kG8ParamTospDontFoolWithSmallKerns")]
		NSString kG8ParamTospDontFoolWithSmallKerns { get; }

		// extern NSString *const kG8ParamTextordInitialxIle;
		[Field ("kG8ParamTextordInitialxIle")]
		NSString kG8ParamTextordInitialxIle { get; }

		// extern NSString *const kG8ParamLanguageModelNgramUseOnlyFirstUft8Step;
		[Field ("kG8ParamLanguageModelNgramUseOnlyFirstUft8Step")]
		NSString kG8ParamLanguageModelNgramUseOnlyFirstUft8Step { get; }

		// extern NSString *const kG8ParamTextordLmsLineTrials;
		[Field ("kG8ParamTextordLmsLineTrials")]
		NSString kG8ParamTextordLmsLineTrials { get; }

		// extern NSString *const kG8ParamTesseditBigramDebug;
		[Field ("kG8ParamTesseditBigramDebug")]
		NSString kG8ParamTesseditBigramDebug { get; }

		// extern NSString *const kG8ParamDocDictPendingThreshold;
		[Field ("kG8ParamDocDictPendingThreshold")]
		NSString kG8ParamDocDictPendingThreshold { get; }

		// extern NSString *const kG8ParamChsTrailingPunct2;
		[Field ("kG8ParamChsTrailingPunct2")]
		NSString kG8ParamChsTrailingPunct2 { get; }

		// extern NSString *const kG8ParamTextordSkewsmoothOffset2;
		[Field ("kG8ParamTextordSkewsmoothOffset2")]
		NSString kG8ParamTextordSkewsmoothOffset2 { get; }

		// extern NSString *const kG8ParamTospFewSamples;
		[Field ("kG8ParamTospFewSamples")]
		NSString kG8ParamTospFewSamples { get; }

		// extern NSString *const kG8ParamTesseditDumpChoices;
		[Field ("kG8ParamTesseditDumpChoices")]
		NSString kG8ParamTesseditDumpChoices { get; }

		// extern NSString *const kG8ParamTextordTestLandscape;
		[Field ("kG8ParamTextordTestLandscape")]
		NSString kG8ParamTextordTestLandscape { get; }

		// extern NSString *const kG8ParamOutlines2;
		[Field ("kG8ParamOutlines2")]
		NSString kG8ParamOutlines2 { get; }

		// extern NSString *const kG8ParamTesseditFixHyphens;
		[Field ("kG8ParamTesseditFixHyphens")]
		NSString kG8ParamTesseditFixHyphens { get; }

		// extern NSString *const kG8ParamMatcherDebugLevel;
		[Field ("kG8ParamMatcherDebugLevel")]
		NSString kG8ParamMatcherDebugLevel { get; }

		// extern NSString *const kG8ParamSuspectConstrain1Il;
		[Field ("kG8ParamSuspectConstrain1Il")]
		NSString kG8ParamSuspectConstrain1Il { get; }

		// extern NSString *const kG8ParamChopSameDistance;
		[Field ("kG8ParamChopSameDistance")]
		NSString kG8ParamChopSameDistance { get; }

		// extern NSString *const kG8ParamClassifyAdaptedPruningFactor;
		[Field ("kG8ParamClassifyAdaptedPruningFactor")]
		NSString kG8ParamClassifyAdaptedPruningFactor { get; }

		// extern NSString *const kG8ParamDocqualExcuseOutlineErrs;
		[Field ("kG8ParamDocqualExcuseOutlineErrs")]
		NSString kG8ParamDocqualExcuseOutlineErrs { get; }

		// extern NSString *const kG8ParamChsLeadingPunct;
		[Field ("kG8ParamChsLeadingPunct")]
		NSString kG8ParamChsLeadingPunct { get; }

		// extern NSString *const kG8ParamClassifyDebugCharacterFragments;
		[Field ("kG8ParamClassifyDebugCharacterFragments")]
		NSString kG8ParamClassifyDebugCharacterFragments { get; }

		// extern NSString *const kG8ParamTesseditLoadSublangs;
		[Field ("kG8ParamTesseditLoadSublangs")]
		NSString kG8ParamTesseditLoadSublangs { get; }

		// extern NSString *const kG8ParamTextordMinBlobHeightFraction;
		[Field ("kG8ParamTextordMinBlobHeightFraction")]
		NSString kG8ParamTextordMinBlobHeightFraction { get; }

		// extern NSString *const kG8ParamTextordEquationDetect;
		[Field ("kG8ParamTextordEquationDetect")]
		NSString kG8ParamTextordEquationDetect { get; }

		// extern NSString *const kG8ParamQualityRejPc;
		[Field ("kG8ParamQualityRejPc")]
		NSString kG8ParamQualityRejPc { get; }

		// extern NSString *const kG8ParamApplyboxExposurePattern;
		[Field ("kG8ParamApplyboxExposurePattern")]
		NSString kG8ParamApplyboxExposurePattern { get; }

		// extern NSString *const kG8ParamTextordShowInitialRows;
		[Field ("kG8ParamTextordShowInitialRows")]
		NSString kG8ParamTextordShowInitialRows { get; }

		// extern NSString *const kG8ParamTospInitGuessXhtMult;
		[Field ("kG8ParamTospInitGuessXhtMult")]
		NSString kG8ParamTospInitGuessXhtMult { get; }

		// extern NSString *const kG8ParamTextordUnderlineWidth;
		[Field ("kG8ParamTextordUnderlineWidth")]
		NSString kG8ParamTextordUnderlineWidth { get; }

		// extern NSString *const kG8ParamStopperAllowableCharacterBadness;
		[Field ("kG8ParamStopperAllowableCharacterBadness")]
		NSString kG8ParamStopperAllowableCharacterBadness { get; }

		// extern NSString *const kG8ParamTospLargeKerning;
		[Field ("kG8ParamTospLargeKerning")]
		NSString kG8ParamTospLargeKerning { get; }

		// extern NSString *const kG8ParamAssumeFixedPitchCharSegment;
		[Field ("kG8ParamAssumeFixedPitchCharSegment")]
		NSString kG8ParamAssumeFixedPitchCharSegment { get; }

		// extern NSString *const kG8ParamTextordMinXheight;
		[Field ("kG8ParamTextordMinXheight")]
		NSString kG8ParamTextordMinXheight { get; }

		// extern NSString *const kG8ParamDawgDebugLevel;
		[Field ("kG8ParamDawgDebugLevel")]
		NSString kG8ParamDawgDebugLevel { get; }

		// extern NSString *const kG8ParamTesseditTessAdaptionMode;
		[Field ("kG8ParamTesseditTessAdaptionMode")]
		NSString kG8ParamTesseditTessAdaptionMode { get; }

		// extern NSString *const kG8ParamTextordShowFinalRows;
		[Field ("kG8ParamTextordShowFinalRows")]
		NSString kG8ParamTextordShowFinalRows { get; }

		// extern NSString *const kG8ParamClassifyMaxNormScaleX;
		[Field ("kG8ParamClassifyMaxNormScaleX")]
		NSString kG8ParamClassifyMaxNormScaleX { get; }

		// extern NSString *const kG8ParamClassifyMaxNormScaleY;
		[Field ("kG8ParamClassifyMaxNormScaleY")]
		NSString kG8ParamClassifyMaxNormScaleY { get; }

		// extern NSString *const kG8ParamTospRule9TestPunct;
		[Field ("kG8ParamTospRule9TestPunct")]
		NSString kG8ParamTospRule9TestPunct { get; }

		// extern NSString *const kG8ParamTospNarrowFraction;
		[Field ("kG8ParamTospNarrowFraction")]
		NSString kG8ParamTospNarrowFraction { get; }

		// extern NSString *const kG8ParamApplyboxLearnNgramsMode;
		[Field ("kG8ParamApplyboxLearnNgramsMode")]
		NSString kG8ParamApplyboxLearnNgramsMode { get; }

		// extern NSString *const kG8ParamCrunchSmallOutlinesSize;
		[Field ("kG8ParamCrunchSmallOutlinesSize")]
		NSString kG8ParamCrunchSmallOutlinesSize { get; }

		// extern NSString *const kG8ParamTesseditRejectDocPercent;
		[Field ("kG8ParamTesseditRejectDocPercent")]
		NSString kG8ParamTesseditRejectDocPercent { get; }

		// extern NSString *const kG8ParamLanguageModelPenaltyPunc;
		[Field ("kG8ParamLanguageModelPenaltyPunc")]
		NSString kG8ParamLanguageModelPenaltyPunc { get; }

		// extern NSString *const kG8ParamTestPtY;
		[Field ("kG8ParamTestPtY")]
		NSString kG8ParamTestPtY { get; }

		// extern NSString *const kG8ParamLoadBigramDawg;
		[Field ("kG8ParamLoadBigramDawg")]
		NSString kG8ParamLoadBigramDawg { get; }

		// extern NSString *const kG8ParamMatcherBadMatchPad;
		[Field ("kG8ParamMatcherBadMatchPad")]
		NSString kG8ParamMatcherBadMatchPad { get; }

		// extern NSString *const kG8ParamTextordLinespaceIqrlimit;
		[Field ("kG8ParamTextordLinespaceIqrlimit")]
		NSString kG8ParamTextordLinespaceIqrlimit { get; }

		// extern NSString *const kG8ParamApplyboxDebug;
		[Field ("kG8ParamApplyboxDebug")]
		NSString kG8ParamApplyboxDebug { get; }

		// extern NSString *const kG8ParamTospImproveThresh;
		[Field ("kG8ParamTospImproveThresh")]
		NSString kG8ParamTospImproveThresh { get; }

		// extern NSString *const kG8ParamTesseditDumpPagesegImages;
		[Field ("kG8ParamTesseditDumpPagesegImages")]
		NSString kG8ParamTesseditDumpPagesegImages { get; }

		// extern NSString *const kG8ParamTextordBaselineDebug;
		[Field ("kG8ParamTextordBaselineDebug")]
		NSString kG8ParamTextordBaselineDebug { get; }

		// extern NSString *const kG8ParamTospShortRow;
		[Field ("kG8ParamTospShortRow")]
		NSString kG8ParamTospShortRow { get; }

		// extern NSString *const kG8ParamXheightPenaltySubscripts;
		[Field ("kG8ParamXheightPenaltySubscripts")]
		NSString kG8ParamXheightPenaltySubscripts { get; }

		// extern NSString *const kG8ParamCrunchEarlyConvertBadUnlvChs;
		[Field ("kG8ParamCrunchEarlyConvertBadUnlvChs")]
		NSString kG8ParamCrunchEarlyConvertBadUnlvChs { get; }

		// extern NSString *const kG8ParamCrunchEarlyMergeTessFails;
		[Field ("kG8ParamCrunchEarlyMergeTessFails")]
		NSString kG8ParamCrunchEarlyMergeTessFails { get; }

		// extern NSString *const kG8ParamTextordInterpolatingSkew;
		[Field ("kG8ParamTextordInterpolatingSkew")]
		NSString kG8ParamTextordInterpolatingSkew { get; }

		// extern NSString *const kG8ParamTesseditLowerFlipHyphen;
		[Field ("kG8ParamTesseditLowerFlipHyphen")]
		NSString kG8ParamTesseditLowerFlipHyphen { get; }

		// extern NSString *const kG8ParamTospOnlySmallGapsForKern;
		[Field ("kG8ParamTospOnlySmallGapsForKern")]
		NSString kG8ParamTospOnlySmallGapsForKern { get; }

		// extern NSString *const kG8ParamMatcherSufficientExamplesForPrototyping;
		[Field ("kG8ParamMatcherSufficientExamplesForPrototyping")]
		NSString kG8ParamMatcherSufficientExamplesForPrototyping { get; }

		// extern NSString *const kG8ParamSuperscriptBetteredCertainty;
		[Field ("kG8ParamSuperscriptBetteredCertainty")]
		NSString kG8ParamSuperscriptBetteredCertainty { get; }

		// extern NSString *const kG8ParamChopEnable;
		[Field ("kG8ParamChopEnable")]
		NSString kG8ParamChopEnable { get; }

		// extern NSString *const kG8ParamCrunchTerribleGarbage;
		[Field ("kG8ParamCrunchTerribleGarbage")]
		NSString kG8ParamCrunchTerribleGarbage { get; }

		// extern NSString *const kG8ParamTesseditPreferJoinedPunct;
		[Field ("kG8ParamTesseditPreferJoinedPunct")]
		NSString kG8ParamTesseditPreferJoinedPunct { get; }

		// extern NSString *const kG8ParamMatcherMinExamplesForPrototyping;
		[Field ("kG8ParamMatcherMinExamplesForPrototyping")]
		NSString kG8ParamMatcherMinExamplesForPrototyping { get; }

		// extern NSString *const kG8ParamTesseditPreserveMinWdLen;
		[Field ("kG8ParamTesseditPreserveMinWdLen")]
		NSString kG8ParamTesseditPreserveMinWdLen { get; }

		// extern NSString *const kG8ParamTesseditPageNumber;
		[Field ("kG8ParamTesseditPageNumber")]
		NSString kG8ParamTesseditPageNumber { get; }

		// extern NSString *const kG8ParamTesseditDisplayOutwords;
		[Field ("kG8ParamTesseditDisplayOutwords")]
		NSString kG8ParamTesseditDisplayOutwords { get; }

		// extern NSString *const kG8ParamTesseditDontRowrejGoodWds;
		[Field ("kG8ParamTesseditDontRowrejGoodWds")]
		NSString kG8ParamTesseditDontRowrejGoodWds { get; }

		// extern NSString *const kG8ParamChopInsideAngle;
		[Field ("kG8ParamChopInsideAngle")]
		NSString kG8ParamChopInsideAngle { get; }

		// extern NSString *const kG8ParamTextordDebugBlob;
		[Field ("kG8ParamTextordDebugBlob")]
		NSString kG8ParamTextordDebugBlob { get; }

		// extern NSString *const kG8ParamTextordAscxRatioMax;
		[Field ("kG8ParamTextordAscxRatioMax")]
		NSString kG8ParamTextordAscxRatioMax { get; }

		// extern NSString *const kG8ParamWordrecDisplaySegmentations;
		[Field ("kG8ParamWordrecDisplaySegmentations")]
		NSString kG8ParamWordrecDisplaySegmentations { get; }

		// extern NSString *const kG8ParamOutputAmbigWordsFile;
		[Field ("kG8ParamOutputAmbigWordsFile")]
		NSString kG8ParamOutputAmbigWordsFile { get; }

		// extern NSString *const kG8ParamTospFuzzyLimitAll;
		[Field ("kG8ParamTospFuzzyLimitAll")]
		NSString kG8ParamTospFuzzyLimitAll { get; }

		// extern NSString *const kG8ParamRejWholeOfMostlyRejectWordFract;
		[Field ("kG8ParamRejWholeOfMostlyRejectWordFract")]
		NSString kG8ParamRejWholeOfMostlyRejectWordFract { get; }

		// extern NSString *const kG8ParamTextordExcessBlobsize;
		[Field ("kG8ParamTextordExcessBlobsize")]
		NSString kG8ParamTextordExcessBlobsize { get; }

		// extern NSString *const kG8ParamRepairUnchoppedBlobs;
		[Field ("kG8ParamRepairUnchoppedBlobs")]
		NSString kG8ParamRepairUnchoppedBlobs { get; }

		// extern NSString *const kG8ParamStopperPhase2CertaintyRejectionOffset;
		[Field ("kG8ParamStopperPhase2CertaintyRejectionOffset")]
		NSString kG8ParamStopperPhase2CertaintyRejectionOffset { get; }

		// extern NSString *const kG8ParamTextordBlshiftMaxshift;
		[Field ("kG8ParamTextordBlshiftMaxshift")]
		NSString kG8ParamTextordBlshiftMaxshift { get; }

		// extern NSString *const kG8ParamTextordBlobSizeBigile;
		[Field ("kG8ParamTextordBlobSizeBigile")]
		NSString kG8ParamTextordBlobSizeBigile { get; }

		// extern NSString *const kG8ParamMinSaneXHtPixels;
		[Field ("kG8ParamMinSaneXHtPixels")]
		NSString kG8ParamMinSaneXHtPixels { get; }

		// extern NSString *const kG8ParamForceWordAssoc;
		[Field ("kG8ParamForceWordAssoc")]
		NSString kG8ParamForceWordAssoc { get; }

		// extern NSString *const kG8ParamTospInitGuessKnMult;
		[Field ("kG8ParamTospInitGuessKnMult")]
		NSString kG8ParamTospInitGuessKnMult { get; }

		// extern NSString *const kG8ParamSegsearchMaxCharWhRatio;
		[Field ("kG8ParamSegsearchMaxCharWhRatio")]
		NSString kG8ParamSegsearchMaxCharWhRatio { get; }

		// extern NSString *const kG8ParamSegsearchMaxPainPoints;
		[Field ("kG8ParamSegsearchMaxPainPoints")]
		NSString kG8ParamSegsearchMaxPainPoints { get; }

		// extern NSString *const kG8ParamChopNewSeamPile;
		[Field ("kG8ParamChopNewSeamPile")]
		NSString kG8ParamChopNewSeamPile { get; }

		// extern NSString *const kG8ParamTextordMaxBlobOverlaps;
		[Field ("kG8ParamTextordMaxBlobOverlaps")]
		NSString kG8ParamTextordMaxBlobOverlaps { get; }

		// extern NSString *const kG8ParamTesseditDebugBlockRejection;
		[Field ("kG8ParamTesseditDebugBlockRejection")]
		NSString kG8ParamTesseditDebugBlockRejection { get; }

		// extern NSString *const kG8ParamTospNearLhEdge;
		[Field ("kG8ParamTospNearLhEdge")]
		NSString kG8ParamTospNearLhEdge { get; }

		// extern NSString *const kG8ParamTospKernGapFactor2;
		[Field ("kG8ParamTospKernGapFactor2")]
		NSString kG8ParamTospKernGapFactor2 { get; }

		// extern NSString *const kG8ParamTospKernGapFactor3;
		[Field ("kG8ParamTospKernGapFactor3")]
		NSString kG8ParamTospKernGapFactor3 { get; }

		// extern NSString *const kG8ParamTospKernGapFactor1;
		[Field ("kG8ParamTospKernGapFactor1")]
		NSString kG8ParamTospKernGapFactor1 { get; }

		// extern NSString *const kG8ParamBidiDebug;
		[Field ("kG8ParamBidiDebug")]
		NSString kG8ParamBidiDebug { get; }

		// extern NSString *const kG8ParamRej1IlTrustPermuterType;
		[Field ("kG8ParamRej1IlTrustPermuterType")]
		NSString kG8ParamRej1IlTrustPermuterType { get; }

		// extern NSString *const kG8ParamTospOldToBugFix;
		[Field ("kG8ParamTospOldToBugFix")]
		NSString kG8ParamTospOldToBugFix { get; }

		// extern NSString *const kG8ParamTesseditRedoXheight;
		[Field ("kG8ParamTesseditRedoXheight")]
		NSString kG8ParamTesseditRedoXheight { get; }

		// extern NSString *const kG8ParamFixspDoneMode;
		[Field ("kG8ParamFixspDoneMode")]
		NSString kG8ParamFixspDoneMode { get; }

		// extern NSString *const kG8ParamTextordBiasedSkewcalc;
		[Field ("kG8ParamTextordBiasedSkewcalc")]
		NSString kG8ParamTextordBiasedSkewcalc { get; }

		// extern NSString *const kG8ParamTospOnlyUseXhtGaps;
		[Field ("kG8ParamTospOnlyUseXhtGaps")]
		NSString kG8ParamTospOnlyUseXhtGaps { get; }

		// extern NSString *const kG8ParamHeuristicMaxCharWhRatio;
		[Field ("kG8ParamHeuristicMaxCharWhRatio")]
		NSString kG8ParamHeuristicMaxCharWhRatio { get; }

		// extern NSString *const kG8ParamPermuteScriptWord;
		[Field ("kG8ParamPermuteScriptWord")]
		NSString kG8ParamPermuteScriptWord { get; }

		// extern NSString *const kG8ParamHeuristicWeightWidth;
		[Field ("kG8ParamHeuristicWeightWidth")]
		NSString kG8ParamHeuristicWeightWidth { get; }

		// extern NSString *const kG8ParamLanguageModelFixedLengthChoicesDepth;
		[Field ("kG8ParamLanguageModelFixedLengthChoicesDepth")]
		NSString kG8ParamLanguageModelFixedLengthChoicesDepth { get; }

		// extern NSString *const kG8ParamPermuteDebug;
		[Field ("kG8ParamPermuteDebug")]
		NSString kG8ParamPermuteDebug { get; }

		// extern NSString *const kG8ParamBestratePruningFactor;
		[Field ("kG8ParamBestratePruningFactor")]
		NSString kG8ParamBestratePruningFactor { get; }

		// extern NSString *const kG8ParamSegsearchMaxFixedPitchCharWhRatio;
		[Field ("kG8ParamSegsearchMaxFixedPitchCharWhRatio")]
		NSString kG8ParamSegsearchMaxFixedPitchCharWhRatio { get; }

		// extern NSString *const kG8ParamSegmentRewardNgramBestChoice;
		[Field ("kG8ParamSegmentRewardNgramBestChoice")]
		NSString kG8ParamSegmentRewardNgramBestChoice { get; }

		// extern NSString *const kG8ParamUseNewStateCost;
		[Field ("kG8ParamUseNewStateCost")]
		NSString kG8ParamUseNewStateCost { get; }

		// extern NSString *const kG8ParamSegmentRewardScript;
		[Field ("kG8ParamSegmentRewardScript")]
		NSString kG8ParamSegmentRewardScript { get; }

		// extern NSString *const kG8ParamTesseditOkMode;
		[Field ("kG8ParamTesseditOkMode")]
		NSString kG8ParamTesseditOkMode { get; }

		// extern NSString *const kG8ParamPermuteChartypeWord;
		[Field ("kG8ParamPermuteChartypeWord")]
		NSString kG8ParamPermuteChartypeWord { get; }

		// extern NSString *const kG8ParamNgramPermuterActivated;
		[Field ("kG8ParamNgramPermuterActivated")]
		NSString kG8ParamNgramPermuterActivated { get; }

		// extern NSString *const kG8ParamSegmentRewardChartype;
		[Field ("kG8ParamSegmentRewardChartype")]
		NSString kG8ParamSegmentRewardChartype { get; }

		// extern NSString *const kG8ParamHeuristicWeightSeamcut;
		[Field ("kG8ParamHeuristicWeightSeamcut")]
		NSString kG8ParamHeuristicWeightSeamcut { get; }

		// extern NSString *const kG8ParamLoadFixedLengthDawgs;
		[Field ("kG8ParamLoadFixedLengthDawgs")]
		NSString kG8ParamLoadFixedLengthDawgs { get; }

		// extern NSString *const kG8ParamEnableNewSegsearch;
		[Field ("kG8ParamEnableNewSegsearch")]
		NSString kG8ParamEnableNewSegsearch { get; }

		// extern NSString *const kG8ParamPermuteFixedLengthDawg;
		[Field ("kG8ParamPermuteFixedLengthDawg")]
		NSString kG8ParamPermuteFixedLengthDawg { get; }

		// extern NSString *const kG8ParamHeuristicWeightRating;
		[Field ("kG8ParamHeuristicWeightRating")]
		NSString kG8ParamHeuristicWeightRating { get; }

		// extern NSString *const kG8ParamHeuristicSegcostRatingBase;
		[Field ("kG8ParamHeuristicSegcostRatingBase")]
		NSString kG8ParamHeuristicSegcostRatingBase { get; }

		// extern NSString *const kG8ParamPermuteOnlyTop;
		[Field ("kG8ParamPermuteOnlyTop")]
		NSString kG8ParamPermuteOnlyTop { get; }

		// extern NSString *const kG8ParamSegmentDebug;
		[Field ("kG8ParamSegmentDebug")]
		NSString kG8ParamSegmentDebug { get; }

		// extern NSString *const kG8ParamSegmentSegcostRating;
		[Field ("kG8ParamSegmentSegcostRating")]
		NSString kG8ParamSegmentSegcostRating { get; }
	}

	// typedef void (^G8RecognitionOperationCallback)(G8Tesseract *);
	delegate void G8RecognitionOperationCallback (G8Tesseract arg0);

	// @interface G8RecognitionOperation : NSOperation
	[BaseType (typeof(NSOperation))]
	interface G8RecognitionOperation
	{
		// @property (readonly, nonatomic, strong) G8Tesseract * tesseract;
		[Export ("tesseract", ArgumentSemantic.Strong)]
		G8Tesseract Tesseract { get; }

		[Wrap ("WeakDelegate")]
		G8TesseractDelegate Delegate { get; set; }

		// @property (nonatomic, weak) id<G8TesseractDelegate> delegate;
		[NullAllowed, Export ("delegate", ArgumentSemantic.Weak)]
		NSObject WeakDelegate { get; set; }

		// @property (readonly, assign, nonatomic) CGFloat progress;
		[Export ("progress", ArgumentSemantic.Assign)]
		nfloat Progress { get; }

		// @property (copy, nonatomic) G8RecognitionOperationCallback recognitionCompleteBlock;
		[Export ("recognitionCompleteBlock", ArgumentSemantic.Copy)]
		G8RecognitionOperationCallback RecognitionCompleteBlock { get; set; }

		// @property (copy, nonatomic) G8RecognitionOperationCallback progressCallbackBlock;
		[Export ("progressCallbackBlock", ArgumentSemantic.Copy)]
		G8RecognitionOperationCallback ProgressCallbackBlock { get; set; }

		// @property (copy) void (^completionBlock)();
		[Export ("completionBlock", ArgumentSemantic.Copy)]
		Action CompletionBlock { get; set; }

		// -(id)initWithLanguage:(NSString *)language;
		[Export ("initWithLanguage:")]
		IntPtr Constructor (string language);
	}
}
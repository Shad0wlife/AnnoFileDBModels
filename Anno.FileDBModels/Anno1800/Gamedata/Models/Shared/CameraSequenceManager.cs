using FileDBSerializing.EncodingAwareStrings;
using FileDBSerializing.ObjectSerializer;

namespace Anno.FileDBModels.Anno1800.Gamedata.Models.Shared
{
    public class CameraSequenceManager
    {

        public int? SequenceCount { get; set; }

        [FlatArray]
        public List<Sequence>? Sequence { get; set; }
    }

    public class Sequence
    {

        public UTF8String? Name { get; set; }
        public int? SequenceID { get; set; }
        public int? FarClipPlane { get; set; }
        public int? Length { get; set; }
        public List<int>? CamerasAtTime { get; set; }
        public int? CamCount { get; set; }
        public int? AssetCount { get; set; }
        public int? ActionCount { get; set; }

        [FlatArray]
        public List<Cam>? Cam { get; set; }
        [FlatArray]
        public List<CameraSequenceAsset>? Asset { get; set; }

        [FlatArray]
        public List<SequenceAction>? SequenceAction { get; set; }
        public int? PreviewAudioCount { get; set; }

        [FlatArray]
        public List<PreviewAudio>? PreviewAudio { get; set; }

    }

    public class Cam
    {
        public int? ID { get; set; }
        public int? Begin { get; set; }
        public int? Length { get; set; }
        public List<List<Keyframe>>? AllKeyframes { get; set; }
        public byte[]? BreakableFirstLastTangents { get; set; }
        public byte[]? SameWeightFirstLastTangents { get; set; }
        public List<bool>? KeyframeTypeVisibility { get; set; }

        //SPECIAL for scenario_02_colony_01
        public int? ConstraintCount { get; set; }

        [FlatArray]
        public List<CamSequenceConstraint>? SequenceConstraint { get; set; }
    }

    public class Keyframe
    {
        public int? Time { get; set; }
        public int? Value { get; set; }
        public float[]? TangentIn { get; set; }
        public float[]? TangentOut { get; set; }
        public byte? Interpolation { get; set; }
        public byte? ManualTangentMode { get; set; }
    }

    public class CamSequenceConstraint
    {
        public ConstraintItem? Constraint { get; set; }
    }

    public class ConstraintItem
    {
        public int? Target { get; set; }
        public int? Begin { get; set; }
        public int? Length { get; set; }
        public List<List<Keyframe>>? AllKeyframes { get; set; }
        public byte[]? BreakableFirstLastTangents { get; set; }
        public byte[]? SameWeightFirstLastTangents { get; set; }
        public List<bool>? KeyframeTypeVisibility { get; set; }
        public int? ID { get; set; }
    }

    public class CameraSequenceAsset
    {
        //Identical to Cam - Inherit?
        //Currently not inherited because different Types and Counts
        public int? ID { get; set; }
        public int? Begin { get; set; }
        public int? Length { get; set; }
        public List<List<Keyframe>>? AllKeyframes { get; set; }
        public byte[]? BreakableFirstLastTangents { get; set; }
        public byte[]? SameWeightFirstLastTangents { get; set; }
        public List<bool>? KeyframeTypeVisibility { get; set; }
        //End Identical to Cam

        public int? id { get; set; }
        public int? GUID { get; set; }
        public int? variation { get; set; }
        public List<int>? animationStarts { get; set; }
        public long? objectlink { get; set; }
        public int? AssetCreationGUIDOverride { get; set; }
        public CameraSequenceAssetOwner? Owner { get; set; }
    }

    public class CameraSequenceAssetOwner
    {
        public short? id { get; set; }
    }

    public class SequenceAction
    {
        public int? ActionType { get; set; }
        public ActionItem? Action { get; set; }
    }

    public class ActionItem
    {
        public int? ID { get; set; }
        public int? TimeTick { get; set; }

        //SPECIAL - Not Like Cam (unlike asset and audio and rest of this)
        public UTF8String? ScriptContent { get; set; }//Might profit from polymorph? If this is set, all following ExecuteOnScriptCancel are not.
        public UnicodeString? ScriptPath { get; set; } //Might profit from polymorph? If this is set, all following ExecuteOnScriptCancel are not.
        public bool? ExecuteOnScriptCancel { get; set; } //Might profit from polymorph? If this is set, all following ExecuteOnScriptCancel are not.

        public int? Begin { get; set; }
        public int? Length { get; set; }
        public List<List<Keyframe>>? AllKeyframes { get; set; }
        public byte[]? BreakableFirstLastTangents { get; set; }
        public byte[]? SameWeightFirstLastTangents { get; set; }
        public List<bool>? KeyframeTypeVisibility { get; set; }
        public UTF8String? SequenceActionName { get; set; }
    }

    public class PreviewAudio
    {
        public int? ID { get; set; }
        public int? Begin { get; set; }
        public int? Length { get; set; }
        public List<List<Keyframe>>? AllKeyframes { get; set; }
        public byte[]? BreakableFirstLastTangents { get; set; }
        public byte[]? SameWeightFirstLastTangents { get; set; }
        public List<bool>? KeyframeTypeVisibility { get; set; }
        public UTF8String? FilePath { get; set; }
    }
}

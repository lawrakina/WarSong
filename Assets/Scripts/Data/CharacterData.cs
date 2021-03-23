using Extension;
using Guirao.UltimateTextDamage;
using Necromancy.UI;
using UnityEngine;


namespace Data
{
    [CreateAssetMenu(fileName = "CharacterData", menuName = "Data/Character Data")]
    public sealed class CharacterData : ScriptableObject
    {
        [SerializeField]
        public GameObject StoragePlayerPrefab;

        [SerializeField]
        public AttachPoint[] ListAttachPoints =
        {
            new AttachPoint()
            {
                Name = StringManager.ATTACH_POINT_LEFT_ONE_WEAPON,
                Path = $"Root/Hips/Spine_01/Spine_02/Spine_03/Clavicle_L/Shoulder_L/Elbow_L/Hand_L",
                LocalPosition = new Vector3(-6.8f, -3.4f, 0.0f),
                LocalRotation = new Vector3(0.0f, -90.0f, -90.0f)
            },
            new AttachPoint()
            {
                Name = StringManager.ATTACH_POINT_RIGHT_ONE_WEAPON,
                Path = $"Root/Hips/Spine_01/Spine_02/Spine_03/Clavicle_R/Shoulder_R/Elbow_R/Hand_R",
                LocalPosition = new Vector3(6.8f, 1.9f, -0.9f),
                LocalRotation = new Vector3(-177.0f, -81.0f, 97.1f)
            },
            new AttachPoint()
            {
                Name = StringManager.ATTACH_POINT_LEFT_SHILD,
                Path = $"Root/Hips/Spine_01/Spine_02/Spine_03/Clavicle_L/Shoulder_L/Elbow_L/Elbow_Attachment_L",
                LocalPosition = new Vector3(0.0f, 4.5f, 0.0f),
                LocalRotation = new Vector3(-90.0f, 0.0f, -90.0f)
            },
        };

        #region Color palette

        [Header("Skin Colors")]
        public Color[] humanSkin = {new Color(1f, 0.8000001f, 0.682353f)};

        public Color[] elfSkin = {new Color(0.5882353f, 0.6274f, 0.73333f)};
        public Color[] orcSkin = {new Color(0.23671f, 0.3294118f, 0.1843f)};

        [Header("Hair Colors")]
        public Color[] allHairAndSubble =
        {
            new Color(0.3098039f, 0.254902f, 0.1764706f), new Color(0.2196079f, 0.2196079f, 0.2196079f),
            new Color(0.8313726f, 0.6235294f, 0.3607843f), new Color(0.8901961f, 0.7803922f, 0.5490196f),
            new Color(0.8000001f, 0.8196079f, 0.8078432f), new Color(0.6862745f, 0.4f, 0.2352941f),
            new Color(0.5450981f, 0.427451f, 0.2156863f), new Color(0.8470589f, 0.4666667f, 0.2470588f),
            new Color(0.3098039f, 0.254902f, 0.1764706f), new Color(0.1764706f, 0.1686275f, 0.1686275f),
            new Color(0.3843138f, 0.2352941f, 0.0509804f), new Color(0.6196079f, 0.6196079f, 0.6196079f),
            new Color(0.6196079f, 0.6196079f, 0.6196079f), new Color(0.9764706f, 0.9686275f, 0.9568628f),
            new Color(0.1764706f, 0.1686275f, 0.1686275f),
            new Color(0.8980393f, 0.7764707f, 0.6196079f)
        };

        [Header("Gear Colors")]
        public Color[] primary =
        {
            new Color(0.2862745f, 0.4f, 0.4941177f), new Color(0.4392157f, 0.1960784f, 0.172549f),
            new Color(0.3529412f, 0.3803922f, 0.2705882f), new Color(0.682353f, 0.4392157f, 0.2196079f),
            new Color(0.4313726f, 0.2313726f, 0.2705882f), new Color(0.5921569f, 0.4941177f, 0.2588235f),
            new Color(0.482353f, 0.4156863f, 0.3529412f), new Color(0.2352941f, 0.2352941f, 0.2352941f),
            new Color(0.2313726f, 0.4313726f, 0.4156863f)
        };

        public Color[] secondary =
        {
            new Color(0.7019608f, 0.6235294f, 0.4666667f), new Color(0.7372549f, 0.7372549f, 0.7372549f),
            new Color(0.1647059f, 0.1647059f, 0.1647059f), new Color(0.2392157f, 0.2509804f, 0.1882353f)
        };

        [Header("Metal Colors")]
        public Color[] metalPrimary =
        {
            new Color(0.6705883f, 0.6705883f, 0.6705883f), new Color(0.5568628f, 0.5960785f, 0.6392157f),
            new Color(0.5568628f, 0.6235294f, 0.6f), new Color(0.6313726f, 0.6196079f, 0.5568628f),
            new Color(0.6980392f, 0.6509804f, 0.6196079f)
        };

        public Color[] metalSecondary =
        {
            new Color(0.3921569f, 0.4039216f, 0.4117647f), new Color(0.4784314f, 0.5176471f, 0.5450981f),
            new Color(0.3764706f, 0.3607843f, 0.3372549f), new Color(0.3254902f, 0.3764706f, 0.3372549f),
            new Color(0.4f, 0.4039216f, 0.3568628f)
        };

        [Header("Leather Colors")]
        public Color[] leatherPrimary = {new Color(0.49f, 0.28f, 0.15f)};

        public Color[] leatherSecondary = {new Color(0.25f, 0.14f, 0.07f)};

        [Header("Body Art Colors")]
        public Color[] bodyArt =
        {
            new Color(0.0509804f, 0.6745098f, 0.9843138f), new Color(0.7215686f, 0.2666667f, 0.2666667f),
            new Color(0.3058824f, 0.7215686f, 0.6862745f), new Color(0.9254903f, 0.882353f, 0.8509805f),
            new Color(0.3098039f, 0.7058824f, 0.3137255f), new Color(0.5294118f, 0.3098039f, 0.6470588f),
            new Color(0.8666667f, 0.7764707f, 0.254902f), new Color(0.2392157f, 0.4588236f, 0.8156863f)
        };

        #endregion
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.AI
{
    public class DummyPanda : MonoBehaviour, IPanda
    {
        public SpriteRenderer firstSelectedToken;
        public SpriteRenderer secondSelectedToken;
        public PandaAnimationsCenter animationCenter;
        [SerializeField] public float Fullness;
        [SerializeField] public float Ero;
        [SerializeField] public float Health;
        [SerializeField] private PandaStats _stats;
        public PandaSpriteSwitcheroo pandaSprites;

        private PandaAnimationState _animationState;
        private Dictionary<PandaAnimationState, Action> _animationStateToAction;

        public void StartAnimationState(PandaAnimationState state)
        {
            _animationState = state;
            _animationStateToAction[state].Invoke();
        }

        public void StopAnimationState(PandaAnimationState state)
        {
            _animationState = PandaAnimationState.Walking;
            _animationStateToAction[_animationState].Invoke();
        }

        public PandaStats GetStats()
        {
            return _stats;
        }

        public void SetStats(PandaStats stats)
        {
            _stats = stats;
        }

        public float GetHealth()
        {
            return Health;
        }

        public void ChangeHealth(float deltaHealth)
        {
            Health += deltaHealth;
            Health = Mathf.Clamp(Health, 0, GameManager.instance.pandaManager.GetMaximumHealth());
        }

        public float GetFullness()
        {
            return Fullness;
        }

        public void ChangeFullness(float deltaFood)
        {
            Fullness += deltaFood;
            Fullness = Mathf.Clamp(Fullness, 0, GameManager.instance.pandaManager.GetMaximumFullness());
        }

        public void GetBodyPartSize(BodyPart part)
        {
            throw new NotImplementedException();
        }

        public bool IsNotFull()
        {
            return GetFullness() < GameManager.instance.pandaManager?.GetMaximumFullness();
        }

        public Gender GetGender()
        {
            return _stats.gender;
        }

        public float GetEro()
        {
            return Ero;
        }

        public void ChangeEro(float deltaEro)
        {
            Ero += deltaEro;
            Ero = Mathf.Clamp(Ero, 0, GameManager.instance.pandaManager.GetMaximumEro());
        }

        public void Start()
        {
            GetStats().name = GameManager.instance.pandaManager.randomNames[UnityEngine.Random.Range(0, GameManager.instance.pandaManager.randomNames.Count)];
            _animationStateToAction = new Dictionary<PandaAnimationState, Action>()
            {
                {PandaAnimationState.Idle, animationCenter.StartIdle},
                {PandaAnimationState.Eating, animationCenter.StartEat},
                {PandaAnimationState.Sexing, animationCenter.StartSexing},
                {PandaAnimationState.Dying, animationCenter.StartDeath},
                {PandaAnimationState.Walking, animationCenter.StartWalk}
            };
            StartAnimationState(PandaAnimationState.Walking);

            if (!GetStats().Genotype.Any())
            {
                GetStats().CreateGenotypeFromPhenotype();
            }
            Color secondaryColor = Color.white;
            switch (GetStats().Phenotype.secondaryColorTrait)
            {
                case PandaLogic.Genetics.SecondaryColorTrait.Black:
                    secondaryColor = new Color(0, 0, 0.2f);
                    break;

                case PandaLogic.Genetics.SecondaryColorTrait.White:
                    secondaryColor = Color.white;
                    break;

                case PandaLogic.Genetics.SecondaryColorTrait.Gray:
                    secondaryColor = Color.gray;
                    break;

                case PandaLogic.Genetics.SecondaryColorTrait.Brown:
                    secondaryColor = new Color(0.60f, 0.20f, 0.00f);
                    break;

                case PandaLogic.Genetics.SecondaryColorTrait.Yellow:
                    secondaryColor = Color.yellow;
                    break;
            }
            pandaSprites.secondaryColor1.color = secondaryColor;
            pandaSprites.secondaryColor2.color = secondaryColor;
            pandaSprites.secondaryColor3.color = secondaryColor;
            pandaSprites.secondaryColor4.color = secondaryColor;
            pandaSprites.secondaryColor5.color = secondaryColor;
            pandaSprites.secondaryColor6.color = secondaryColor;

            Color primaryColor = Color.white;
            switch (GetStats().Phenotype.primaryBodyColorTrait)
            {
                case PandaLogic.Genetics.PrimaryBodyColorTrait.Black:
                    primaryColor = new Color(0, 0, 0.2f);
                    break;

                case PandaLogic.Genetics.PrimaryBodyColorTrait.White:
                    primaryColor = Color.white;
                    break;

                case PandaLogic.Genetics.PrimaryBodyColorTrait.Gray:
                    primaryColor = Color.gray;
                    break;

                case PandaLogic.Genetics.PrimaryBodyColorTrait.Brown:

                    primaryColor = new Color(0.60f, 0.20f, 0.00f);

                    break;

                case PandaLogic.Genetics.PrimaryBodyColorTrait.Yellow:
                    primaryColor = Color.yellow;
                    break;
            }

            pandaSprites.primaryColor1.color = primaryColor;
            pandaSprites.primaryColor2.color = primaryColor;
            pandaSprites.primaryColor3.color = primaryColor;
            pandaSprites.primaryColor4.color = primaryColor;
            pandaSprites.primaryColorFat.color = primaryColor;

            switch (GetStats().Phenotype.bodyTypeTrait)
            {
                case PandaLogic.Genetics.BodyTypeTrait.Nomral:
                    pandaSprites.bodyRenderer1.sprite = pandaSprites.slimBody1;
                    pandaSprites.bodyRenderer2.sprite = pandaSprites.slimBody2;
                    pandaSprites.bodyRenderer3.sprite = pandaSprites.slimBody3;
                    pandaSprites.bodyRendererLines.sprite = pandaSprites.slimBodyLines;
                    pandaSprites.bodyRendererFat.sprite = pandaSprites.blankSprite;
                    pandaSprites.bodyRendererLinesFat.sprite = pandaSprites.blankSprite;
                    break;

                case PandaLogic.Genetics.BodyTypeTrait.Fat:
                    pandaSprites.bodyRendererFat.sprite = pandaSprites.fatBody;
                    pandaSprites.bodyRendererLinesFat.sprite = pandaSprites.fatBodyLines;
                    pandaSprites.bodyRenderer1.sprite = pandaSprites.blankSprite;
                    pandaSprites.bodyRenderer2.sprite = pandaSprites.blankSprite;
                    pandaSprites.bodyRenderer3.sprite = pandaSprites.blankSprite;
                    pandaSprites.bodyRendererLines.sprite = pandaSprites.blankSprite;
                    break;
            }

            switch (GetStats().Phenotype.noseMouthTypeTrait)
            {
                case PandaLogic.Genetics.NoseMouthTypeTrait.Heart:
                    pandaSprites.mouthLine.sprite = pandaSprites.mouthHeart;
                    pandaSprites.noseLine.sprite = pandaSprites.noseHeart;
                    pandaSprites.mouthColor.sprite = pandaSprites.blankSprite;
                    break;

                case PandaLogic.Genetics.NoseMouthTypeTrait.Normal:
                    pandaSprites.mouthLine.sprite = pandaSprites.mouthNormal;
                    pandaSprites.noseLine.sprite = pandaSprites.noseNormal;
                    pandaSprites.mouthColor.sprite = pandaSprites.mouthColorNormal;
                    break;

                case PandaLogic.Genetics.NoseMouthTypeTrait.Pig:
                    pandaSprites.mouthLine.sprite = pandaSprites.mouthPig;
                    pandaSprites.noseLine.sprite = pandaSprites.nosePig;
                    pandaSprites.mouthColor.sprite = pandaSprites.blankSprite;
                    break;

                case PandaLogic.Genetics.NoseMouthTypeTrait.Pink:
                    pandaSprites.mouthLine.sprite = pandaSprites.mouthPink;
                    pandaSprites.noseLine.sprite = pandaSprites.nosePink;
                    pandaSprites.mouthColor.sprite = pandaSprites.blankSprite;
                    break;

                case PandaLogic.Genetics.NoseMouthTypeTrait.Small:
                    pandaSprites.mouthLine.sprite = pandaSprites.mouthSmall;
                    pandaSprites.noseLine.sprite = pandaSprites.noseSmall;
                    pandaSprites.mouthColor.sprite = pandaSprites.blankSprite;
                    break;

                case PandaLogic.Genetics.NoseMouthTypeTrait.Triangle:
                    pandaSprites.mouthLine.sprite = pandaSprites.mouthTriangle;
                    pandaSprites.noseLine.sprite = pandaSprites.noseTriangle;
                    pandaSprites.mouthColor.sprite = pandaSprites.blankSprite;
                    break;
            }

            switch (GetStats().Phenotype.tailTypeTrait)
            {
                case PandaLogic.Genetics.TailTypeTrait.Normal:
                    pandaSprites.tailColor.sprite = pandaSprites.tailNomralColor;
                    pandaSprites.tailLine.sprite = pandaSprites.tailNomralLine;
                    break;

                case PandaLogic.Genetics.TailTypeTrait.Cat:
                    pandaSprites.tailColor.sprite = pandaSprites.tailCatColor;
                    pandaSprites.tailLine.sprite = pandaSprites.tailCatLine;
                    break;

                case PandaLogic.Genetics.TailTypeTrait.Electric:
                    pandaSprites.tailColor.sprite = pandaSprites.tailElectricColor;
                    pandaSprites.tailLine.sprite = pandaSprites.tailElectricLine;
                    break;

                case PandaLogic.Genetics.TailTypeTrait.Fuzzy:
                    pandaSprites.tailColor.sprite = pandaSprites.tailFuzzyColor;
                    pandaSprites.tailLine.sprite = pandaSprites.tailFuzzyLine;
                    break;

                case PandaLogic.Genetics.TailTypeTrait.Pig:
                    pandaSprites.tailColor.sprite = pandaSprites.blankSprite;
                    pandaSprites.tailLine.sprite = pandaSprites.tailPigLine;
                    break;
            }
            switch (GetStats().Phenotype.specialTrait)
            {
                case PandaLogic.Genetics.SpecialTrait.Without:
                    pandaSprites.special1.enabled = false;
                    pandaSprites.special2.enabled = false;
                    break;

                case PandaLogic.Genetics.SpecialTrait.Butt:
                    pandaSprites.special1.enabled = true;
                    pandaSprites.special2.enabled = false;
                    break;

                case PandaLogic.Genetics.SpecialTrait.Face:
                    pandaSprites.special1.enabled = true;
                    pandaSprites.special2.enabled = false;
                    break;

                case PandaLogic.Genetics.SpecialTrait.Both:
                    pandaSprites.special1.enabled = true;
                    pandaSprites.special2.enabled = false;
                    break;
            }

            switch (GetStats().Phenotype.earTypeTrait)
            {
                case PandaLogic.Genetics.EarTypeTrait.Nomral:
                    pandaSprites.earLeftColor.sprite = pandaSprites.earLeftColorNormal;
                    pandaSprites.earLeftLine.sprite = pandaSprites.earLeftLineNormal;
                    pandaSprites.earRightColor.sprite = pandaSprites.earRightColorNormal;
                    pandaSprites.earRightLine.sprite = pandaSprites.earRightLineNormal;
                    pandaSprites.earRightFilling.sprite = pandaSprites.earLeftFillingNormal;
                    pandaSprites.earLeftFilling.sprite = pandaSprites.earRightFillingNormal;
                    break;

                case PandaLogic.Genetics.EarTypeTrait.Curved:
                    pandaSprites.earLeftColor.sprite = pandaSprites.earLeftColorCurved;
                    pandaSprites.earLeftLine.sprite = pandaSprites.earLeftLineCurved;
                    pandaSprites.earRightColor.sprite = pandaSprites.earRightColorCurved;
                    pandaSprites.earRightLine.sprite = pandaSprites.earRightLineCurved;
                    pandaSprites.earRightFilling.sprite = pandaSprites.blankSprite;
                    pandaSprites.earLeftFilling.sprite = pandaSprites.blankSprite;
                    break;

                case PandaLogic.Genetics.EarTypeTrait.Long:
                    pandaSprites.earLeftColor.sprite = pandaSprites.earLeftColorLong;
                    pandaSprites.earLeftLine.sprite = pandaSprites.earLeftLineLong;
                    pandaSprites.earRightColor.sprite = pandaSprites.earRightColorLong;
                    pandaSprites.earRightLine.sprite = pandaSprites.earRightLineLong;
                    pandaSprites.earRightFilling.sprite = pandaSprites.blankSprite;
                    pandaSprites.earLeftFilling.sprite = pandaSprites.blankSprite;
                    break;

                case PandaLogic.Genetics.EarTypeTrait.Small:
                    pandaSprites.earLeftColor.sprite = pandaSprites.earLeftColorSmall;
                    pandaSprites.earLeftLine.sprite = pandaSprites.earLeftLineSmall;
                    pandaSprites.earRightColor.sprite = pandaSprites.earRightColorSmall;
                    pandaSprites.earRightLine.sprite = pandaSprites.earRightLineSmall;
                    pandaSprites.earRightFilling.sprite = pandaSprites.blankSprite;
                    pandaSprites.earLeftFilling.sprite = pandaSprites.blankSprite;
                    break;
            }

            switch (GetStats().Phenotype.eyeColorTrait)
            {
                case PandaLogic.Genetics.EyeColorTrait.Brown:
                    pandaSprites.eyeColor1.color = new Color(150, 75, 0);
                    pandaSprites.eyeColor2.color = new Color(150, 75, 0);
                    break;

                case PandaLogic.Genetics.EyeColorTrait.Red:
                    pandaSprites.eyeColor1.color = Color.red;
                    pandaSprites.eyeColor2.color = Color.red;
                    break;

                case PandaLogic.Genetics.EyeColorTrait.White:
                    pandaSprites.eyeColor1.color = Color.white;
                    pandaSprites.eyeColor2.color = Color.white;
                    break;

                case PandaLogic.Genetics.EyeColorTrait.Blue:
                    pandaSprites.eyeColor1.color = Color.blue;
                    pandaSprites.eyeColor2.color = Color.blue;
                    break;
            }

            switch (GetStats().Phenotype.eyesTypeTrait)
            {
                case PandaLogic.Genetics.EyesTypeTrait.Big:
                    pandaSprites.eyeLine1.sprite = pandaSprites.eye1Big;
                    pandaSprites.eyeLine2.sprite = pandaSprites.eye2Big;
                    break;
                /*
            case PandaLogic.Genetics.EyesTypeTrait.Crazy:
                pandaSprites.eyeLine1.sprite = pandaSprites.eye1Crazy;
                pandaSprites.eyeLine2.sprite = pandaSprites.eye2Crazy;
                break;

            case PandaLogic.Genetics.EyesTypeTrait.Triangle:
                pandaSprites.eyeLine1.sprite = pandaSprites.eye1Triangle;
                pandaSprites.eyeLine2.sprite = pandaSprites.eye2Triangle;
                break;
                */
                case PandaLogic.Genetics.EyesTypeTrait.Normal:
                    pandaSprites.eyeLine1.sprite = pandaSprites.eye1Normal;
                    pandaSprites.eyeLine2.sprite = pandaSprites.eye2Normal;

                    break;
            }

            GameManager.instance.pandaManager.pandasOnDisplay.Add(this);
        }

        private void OnDestroy()
        {
            if (GameManager.instance != null && GameManager.instance.pandaManager != null)
            {
                GameManager.instance.pandaManager.pandasOnDisplay.Remove(this);
            }
        }

        public void Select(int i)
        {
            switch (i)
            {
                case 1:
                    firstSelectedToken.gameObject.SetActive(true);
                    break;

                case 2:
                    secondSelectedToken.gameObject.SetActive(true);
                    break;

                default:
                    break;
            }
            GameManager.instance.notificationManager.OnPandaSelected(this);
        }

        public void Deselect(int i)
        {
            switch (i)
            {
                case 1:
                    firstSelectedToken.gameObject.SetActive(false);
                    break;

                case 2:
                    secondSelectedToken.gameObject.SetActive(false);
                    break;

                default:
                    break;
            }
        }

        public void GoToForest()
        {
            //TODO: bezpiecznie wycofac sie ze wszystkich obecnych interacji
            this.gameObject.SetActive(false);
        }
    }

    public enum PandaAnimationState
    {
        Idle, Dying, Walking, Sexing, Eating
    }
}
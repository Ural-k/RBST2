using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerUI : MonoBehaviour
{
    [SerializeField] private Player player_;
    [SerializeField] private EffectIcon effectImageTemple_;
    [SerializeField] private Transform buffParent_, debuffParent_;
    private List<EffectIcon> buffImage_ = new List<EffectIcon>();
    private List<EffectIcon> debuffImage_ = new List<EffectIcon>();
    private List<EffectController> effects_ = new List<EffectController>();
    private Dictionary<EffectController, EffectIcon> effects2 = new Dictionary<EffectController, EffectIcon>();

    private void Start()
    {
        player_.Effect.OnEffectAdd += AddEffect;
        player_.Effect.OnEffectRemove += RemoveEffect;
    }

    private void AddEffect(EffectController effect)
    {
        if (effects2.Any(n => n.Key == effect)) //すでにある場合
        {
            //スタック表示の変更
        }
        else//ない場合
        {
            effects2.Add(effect, new EffectIcon());

            foreach(var e in effects2)
            {
                
            }
        }

        /*
         * やりたいこと
         * 
         * 付与時
         * 　リストを整える
         * 　　1.引数(呼び出し時に追加されたバフ)を現在のDictionaryにAdd()。:EffectIconを基準にすると、追加した順に左から数えれる
         * 　for文で順番に表示
         * 　　1.テンプレが置いてない(iの位置にDictionaryがない)場合、Instantiate()で呼び出す
         * 　　2.テンプレが置いてある(iの位置にDictionaryがある)場合、Active()でsprite変更+スタック表示
         * 
         * 剥奪時
         * 　引数のEffectIconのα値を0にしてからリムーヴ
         * 
         * 
         * 
         * 
         * 
         * 
         * 
         */



        effects_.Add(effect);

        int buffnum = 0;
        int debuffnum = 0;
        foreach (var e in effects_)
        {
            if (e.data_.type_ == EffectType.Buff)
            {
                if (buffnum >= buffImage_.Count)//生成してない場合
                {
                    var ins = Instantiate(effectImageTemple_, buffParent_);
                    ins.transform.localPosition = new Vector2(30 * buffnum, 0);
                    buffImage_.Add(ins);
                }
                buffImage_[buffnum].Active(e);
                ++buffnum;
            }
            else if (e.data_.type_ == EffectType.Debuff)
            {
                if (debuffnum >= debuffImage_.Count)
                {
                    var ins = Instantiate(effectImageTemple_, debuffParent_);
                    ins.transform.localPosition = new Vector2(30 * debuffnum, 0);
                    debuffImage_.Add(ins);
                }
                debuffImage_[debuffnum].Active(e);
                ++debuffnum;
            }
        }
    }

    private void RemoveEffect(EffectController effect)
    {
        int buffnum = 0;
        int debuffnum = 0;
        foreach (var e in effects_)
        {
            if (e.data_.type_ == EffectType.Buff)
            {
                buffImage_[buffnum].Passive();
                ++buffnum;
            }
            else if (e.data_.type_ == EffectType.Debuff)
            {
                debuffImage_[debuffnum].Passive();
                ++debuffnum;
            }
        }
        effects_.Remove(effect);
    }

    //--オブジェクトプール------------------------------------(別のクラスに分ける予定(AwakeをInitializeにする))//開発中:バフUIを消すに当たって、EffectControllerの情報をEffectImageに入れなくてはいけない。また、EffectImageの矢印5個分をEffectImageにまとめて入れたい

    //private UnityEngine.Pool.ObjectPool<EffectImage> pool_;

    //private void Awake()
    //{
    //    pool_ = new UnityEngine.Pool.ObjectPool<EffectImage>(
    //        () => Instantiate(prefab_, parent_),//新規生成時
    //        item => item.Active(true),        //Get
    //        item => item.Active(false),       //Release
    //        item => Destroy(item.gameObject)    //上限や破棄時
    //    );
    //}

    //public EffectImage Spawn(Vector2 pos, EffectController effect)
    //{
    //    var item = pool_.Get();
    //    item.Initialize(pos, effect);
    //    return item;
    //}

    //public void Despawn(EffectImage item) => pool_.Release(item);

    //public void Active(List<EffectController> effects)
    //{
    //    //// 不要になったら
    //    //effectPool_.Despawn(item);

    //}
    //------------------------------------------------------------
}

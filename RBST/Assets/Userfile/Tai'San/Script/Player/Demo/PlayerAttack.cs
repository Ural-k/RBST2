using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    /*
     * <アタック仕様>
     * 最初は2コンボ(or1コンボ)進むにつれてコンボ数が増えてく(選択型,lvごと,コンボは)
     * コンボは入手したコンボアクションからランダム。(コンボ数ごと)
     * つまり、コンボを獲得すればするほど基本的には強くなる
     * 強化案として特定のコンボ数でアビリティがはさまれる、攻撃力が上がる、攻撃スキルの確率が上がる
     * 簡単操作だが判断することもある程度の攻撃システム
     * 増えてくコンボにはジョブ適正がある(設定的におかしい攻撃をなくすため)
     * 
     * 例:近接物理,近接魔法,遠隔物理,遠隔魔法,タンク,ヒーラー,攻撃職,すべてetc
     * 条件攻撃:カウンター成功時のみ変化,ヒール時変化,確率攻撃発動時変化,軽減成功時変化
     * 
     * <用意するプログラム>
     * ・変数
     * コンボ数
     * コンボ数ごとの情報をまとめるもの(struct?)
     *      ↑
     * ・structの内容
     * 持ってるコンボのList
     * 
     * 
     * 
     */

    int combo = 1;
    int nowCombo = 0;
    List<Attack> atk;
    
    void ChoiceAttack()
    {

    }


    struct Attack
    {
        int power;
        
    }
}


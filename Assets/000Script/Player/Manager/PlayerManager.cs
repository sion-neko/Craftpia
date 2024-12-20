using System.Collections;
using System.Collections.Generic;


//������
public class PlayerManager : IManager
{
    IItemConsumption _bag;
    ICookItemSozaiAcquisition _cookItemSozaiAcquisition;


    public PlayerManager(ICookItemSozaiAcquisition cookItemSozaiAcquisition)
    {
        _bag = new Bag();
        _cookItemSozaiAcquisition = cookItemSozaiAcquisition;

    }


    //�o�b�N�̍ޗ�������ė������o�b�N�ɒǉ�
    public void doCook(string cookItem_id)
    {
        //���V�s�̏����Ђς��Ă���
        Sozai[] sozais = _cookItemSozaiAcquisition.getCookItemSozai(cookItem_id);

        //�o�b�N����f�ނ�����(���m�F��canCook�ōs��)
        foreach (Sozai need_sozai in sozais)
        {
            _bag.subItemQuantity(need_sozai.id, need_sozai.num);
        }

        //����͂܂Ƃ߂Ē����ł��Ȃ��̂Œ������cook_item�͈��
        _bag.inItem(cookItem_id, 1);

    }

    public void pickUpItem(string item_id, int quantity)//item���E��
    {
        //List<string> inItemArgumentList = new List<string>(_bag.createInItemArgumentList());
        //if (_bag.isMaxBag() && !inItemArgumentList.Contains(item_id)) {
        //    return;
        //}
        _bag.inItem(item_id, quantity);
    }

    public Dictionary<string, int> getBagSummary() { return _bag.getBagSummary(); }

    public Dictionary<Item, int>�@getUseItem() { return new Dictionary<Item, int> {}; }
}

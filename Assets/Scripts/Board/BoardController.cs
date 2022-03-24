using System.Collections.Generic;

public class BoardController {
    private BoardView _view;
    private BoardModel _model;
    private List<CardController> _cards = new List<CardController>();

    public BoardController(BoardView view, BoardModel model) {
        _model = model;
        _view = view;
    }

    public void AddCard(CardController cardController) {
        _view.PositionCardView(cardController.View, _cards.Count);
        _cards.Add(cardController);
    }
}
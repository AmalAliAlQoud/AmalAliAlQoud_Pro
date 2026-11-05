import 'dart:convert';
import 'package:http/http.dart' as http;
import 'ConectionString.dart';

class SpeciallyService {
  Future<List<dynamic>> getSpecialty() async {
    try {
      final response = await http.get(
        Uri.parse(ConectionString.Specially),
      );

      print('Response status: ${response.statusCode}');
      print('Response body: ${response.body}');

      if (response.statusCode == 200) {
        final data = jsonDecode(response.body);

        if (data is Map && data.containsKey('specialty')) {
          return data['specialty'] ?? [];
        }

        if (data is List) {
          return data;
        }
      }
    } catch (e) {
      print('Error fetching Specialty: $e');
    }
    return [];
  }
}